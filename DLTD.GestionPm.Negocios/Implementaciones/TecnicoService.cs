using DLTD.GestionPm.AccesoDatos.Configuracion;
using DLTD.GestionPm.AccesoDatos.Contexto;
using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Marca;
using DLTD.GestionPm.Dto.Request.Tecnico;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Marca;
using DLTD.GestionPm.Dto.Response.Tecnico;
using DLTD.GestionPm.Entidad;
using DLTD.GestionPm.Negocios.Interfaces;
using DLTD.GestionPm.Repositorios.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Implementaciones
{
    public class TecnicoService: ITecnicoService
    {
        private readonly UserManager<SecurityEntity> _userManager;
        private readonly IUnitOfWork _uow;
        private readonly IEmailService _email;
        private readonly ILogger<TecnicoService> _logger;

        public TecnicoService(UserManager<SecurityEntity> userManager, IUnitOfWork uow, IEmailService email, ILogger<TecnicoService> logger)
        {            
            _uow = uow;
            _userManager = userManager;
            _email = email;
            _logger = logger;
        }

        public async Task<BaseResponse> AddAsync(TecnicoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var tecnicoEntity = request.Adapt<Tecnico>();
                var (tecnico, identityResult) = await _uow.TecnicoRepo.CreateAsync(tecnicoEntity, request.NoIdentificacion, request.Clave);
                
                if (!identityResult.Succeeded)
                {
                    response.IsSuccess = false;
                    response.Message = string.Join(" | ", identityResult.Errors.Select(e => e.Description));
                    return response;
                }

                response.IsSuccess = true;
                response.Message = "Tecnico registrado correctamente.";
                await _email.SendEmail(tecnicoEntity.Email!, "Registro al Sistema Integrado de PM", CreateTemplate(request));
                    
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al registrar el tecnico";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        private string CreateTemplate(TecnicoRequest request)
        {
            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates/Email/Bienvenida.html");
            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException("Template not found", templatePath);
            }

            var template = File.ReadAllText(templatePath);
            template = template.Replace("{{name}}", $"{request.Nombres} {request.Apellidos}");
            template = template.Replace("{{usuario}}", request.NoIdentificacion);
            template = template.Replace("{{contraseña}}", request.Clave);
            template = template.Replace("{{logo_url}}", $"");
            template = template.Replace("{{login_link}}", $"");

            return template;
        }

        public async Task<BaseResponse> UpdateAsync(int id, TecnicoRequest request)
        {
            var response = new BaseResponse();
            

            try
            {
                var tecnico = await _uow.TecnicoRepo.FindAsync(id);
                if (tecnico == null) throw new InvalidDataException("Tecnico no encontrado.");

                request.Adapt(tecnico);
                

                // Actualizamos el usuario de Identity
                var user = await _userManager.Users.FirstOrDefaultAsync(p => p.IdUsuario == id);
                if (user != null)
                {
                    user.Email = tecnico.Email;
                    user.UserName = tecnico.NoIdentificacion;
                    user.NombresCompletos = $"{tecnico.Nombres} {tecnico.Apellidos}";
                    user.Codigo = tecnico.Codigo;

                    var identityResult = await _userManager.UpdateAsync(user);
                    if (!identityResult.Succeeded)
                    {
                        response.Message = string.Join(" | ", identityResult.Errors.Select(e => e.Description));
                        return response;
                    }
                }

                // Si todo Identity salió bien, guardamos los cambios de la tabla Técnico
                await _uow.SaveAsync();
                response.IsSuccess = true;
                response.Message = "Tecnico actualizado exitosamente.";
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al actualizar el tecnico";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<TecnicoResponse>> FindByIdAsync(int id)
        {
            var response = new BaseResponse<TecnicoResponse>();
            try
            {
                var tecnico = await _uow.TecnicoRepo.FindAsync(id);
                if (tecnico == null) throw new InvalidDataException("Tecnico no encontrado.");

                response.IsSuccess = true;
                response.Result = tecnico.Adapt<TecnicoResponse>();
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al buscar la marca.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<PaginationResponse<ListaTecnicoResponse>> ListaAsync(PaginationRequest request)
        {
            var response = new PaginationResponse<ListaTecnicoResponse>();
            try
            {
                var result = await _uow.TecnicoRepo.ListAsync(
                        predicate: p => p.Status != "Eliminado" &&
                                        (string.IsNullOrEmpty(request.Filter) || p.NoIdentificacion.Contains(request.Filter) || p.Codigo.Contains(request.Filter) ||
                                        p.Nombres.Contains(request.Filter) || p.Apellidos.Contains(request.Filter) || p.Status.Contains(request.Filter)),
                        selector: p => new ListaTecnicoResponse
                        {
                            Id = p.Id,
                            Nombres = p.Nombres ?? "",
                            Apellidos = p.Apellidos ?? "",
                            Email = p.Email ?? "",
                            TurnoActual = p.TurnoActual ?? "",
                            NoIdentificacion = p.NoIdentificacion ?? "",
                            Telefono = p.Telefono ?? "",
                            Codigo = p.Codigo ?? "",
                            Especialidad = p.Especialidad ?? "",
                            Status = p.Status ?? ""
                        },
                        page: request.Page,
                        rows: request.Rows,
                        orderBy: p => p.Id
                    );

                response.IsSuccess = true;
                response.Result = result.Result;
                response.TotalRows = result.TotalElements;
                response.TotalPages = (int)Math.Ceiling((double)result.TotalElements / request.Rows);
            }
            catch (Exception ex)
            {
                response.Message = ex.ToString();

                response.Message = "Hubo un error al listar los tecnicos.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<TecnicoResponse>> DeleteAsync(int id)
        {
            var response = new BaseResponse<TecnicoResponse>();
            try
            {
                var tecnico = await _uow.TecnicoRepo.FindAsync(id);
                if (tecnico == null) throw new InvalidDataException("Tecnico no encontrado.");

                await _uow.TecnicoRepo.DeleteAsync(id);
                response.IsSuccess = true;
                response.Message = "Tecnico eliminado correctamente.";

            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al eliminar la marca.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }
    }
}
