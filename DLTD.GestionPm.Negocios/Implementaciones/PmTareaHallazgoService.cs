using Azure.Core;
using DLTD.GestionPm.Comun;
using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.PmTareaHallazgo;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.PmTareaHallazgo;
using DLTD.GestionPm.Dto.Response.Ruta;
using DLTD.GestionPm.Entidad;
using DLTD.GestionPm.Negocios.Interfaces;
using DLTD.GestionPm.Repositorios.Interfaces;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Implementaciones
{
    public class PmTareaHallazgoService: IPmTareaHallazgoService
    {
        private readonly IUnitOfWork _uow;        
        private readonly ILogger<PmTareaHallazgo> _logger;
        private readonly IConfiguration _config;
        private readonly IAzureBlobService _azure;

        public PmTareaHallazgoService(IUnitOfWork uow, ILogger<PmTareaHallazgo> logger, IConfiguration config, IAzureBlobService azure)
        {
            _uow = uow;
            _logger = logger;            
            _config = config;
            _azure = azure;
        }

        public async Task<BaseResponse> AddAsync(PmTareaHallazgoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                
                var azureConfig = _config.GetSection("AzureSettings");
                var nuevo = request.Adapt<PmTareaHallazgo>();

                nuevo.ImagenUrl = $"{azureConfig["UrlBlob"]}/{request.Soporte.Name}";

                var hallazgo = await _uow.PmTareaHallazgoRepo.AddAsync(nuevo);
                if( hallazgo != null )
                {
                    await _azure.SaveResourceBlob(request.Soporte);
                }

                await _uow.SaveAsync();
                response.IsSuccess = true;
                response.Message = "Hallazgo registrada exitosamente.";
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al registrar el Hallazgo";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }
        public async Task<BaseResponse> UpdateAsync(int id, PmTareaHallazgoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var PmTareaHallazgo = await _uow.PmTareaHallazgoRepo.FindAsync(id);
                if (PmTareaHallazgo == null) throw new InvalidDataException("Hallazgo no encontrada");

                if (!string.IsNullOrEmpty(request.Soporte?.Base64))
                {
                    // Si hay foto nueva, la subimos a Azure
                    await _azure.SaveResourceBlob(request.Soporte);

                    // Actualizamos la URL en la entidad
                    var azureConfig = _config.GetSection("AzureSettings");
                    PmTareaHallazgo.ImagenUrl = $"{azureConfig["UrlBlob"]}/{request.Soporte.Name}";
                }

                request.Adapt(PmTareaHallazgo);
                // Si el Adapt te da problemas porque te borra la URL, puedes hacerlo manual:
                // entidad.Descripcion = request.Descripcion;
                // entidad.IdTipoHallazgo = request.IdTipoHallazgo;
                // entidad.Status = request.Status;
                // entidad.FechaHallazgo = request.FechaHallazgo;
                await _uow.SaveAsync();

                response.IsSuccess = true;
                response.Message = "Hallazgo actualizado exitosamente.";
            }
            catch(InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al actualizar la PmTareaHallazgo";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }
        public async Task<BaseResponse<PmTareaHallazgoResponse>> FindByIdAsync(int id)
        {
            var response = new BaseResponse<PmTareaHallazgoResponse>();
            try
            {
                var PmTareaHallazgo = await _uow.PmTareaHallazgoRepo.FindAsync(id);
                if (PmTareaHallazgo == null) throw new InvalidDataException("Hallazgo no encontrada");

                response.IsSuccess = true;
                response.Result = PmTareaHallazgo.Adapt<PmTareaHallazgoResponse>();
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al buscar la PmTareaHallazgo.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }        
        public async Task<PaginationResponse<ListaPmTareaHallazgoResponse>> ListaAsync(PaginationRequest request)
        {
            var response = new PaginationResponse<ListaPmTareaHallazgoResponse>();
            try
            {
                var result = await _uow.PmTareaHallazgoRepo.ListAsync(
                        predicate: p => p.Status != "Eliminado" && 
                        (string.IsNullOrEmpty(request.Filter)  || p.Descripcion!.Contains(request.Filter) || p.Status.Contains(request.Filter)),
                        selector: p => new ListaPmTareaHallazgoResponse
                        {
                            Id = p.Id,
                            IdPmDetalle = p.IdPmDetalle,
                            NoEquipo = p.NoEquipo,
                            TipoHallazgo = p.IdTipoHallazgoNavigation.Nombre,
                            ImagenUrl = p.ImagenUrl,                                                        
                            Descripcion = p.Descripcion,
                            FechaHallazgo = p.FechaHallazgo,
                            Tecnicos = p.Tecnicos,
                            ValidadoPor = p.ValidadoPor!,
                            Status = p.Status,
                            UsuarioRegistro = p.UsuarioRegistro,
                            FechaRegistro = p.FechaRegistro
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
                response.Message = "Hubo un error al listar las PmTareaHallazgos.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }
        public async Task<BaseResponse<PmTareaHallazgoResponse>> DeleteAsync(int id)
        {
            var response = new BaseResponse<PmTareaHallazgoResponse>();
            try
            {
                var PmTareaHallazgo = await _uow.PmTareaHallazgoRepo.FindAsync(id);
                if (PmTareaHallazgo == null) throw new InvalidDataException("Hallazgo no encontrada");

                await _uow.PmTareaHallazgoRepo.DeleteAsync(id);
                response.IsSuccess = true;
                response.Message = "Hallazgo eliminada correctamente.";
                
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al eliminar la PmTareaHallazgo.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }


        public async Task<BaseResponse<string>> GetNoEquipo(int idPmDetalle)
        {
            var response = new BaseResponse<string>();            
            var noEquipo = await _uow.PmTareaHallazgoRepo.FindEquipo(idPmDetalle);

            if (!string.IsNullOrEmpty(noEquipo))
            {
                response.IsSuccess = true;
                response.Result = noEquipo;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Equipo no encontrado.";
            }
            return response;
        }

    }
}
