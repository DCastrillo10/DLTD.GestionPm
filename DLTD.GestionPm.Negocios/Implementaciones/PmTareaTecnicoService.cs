using Azure.Core;
using DLTD.GestionPm.Comun;
using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.PmTareaTecnico;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.PmTareaTecnico;
using DLTD.GestionPm.Dto.Response.Ruta;
using DLTD.GestionPm.Entidad;
using DLTD.GestionPm.Negocios.Interfaces;
using DLTD.GestionPm.Repositorios.Interfaces;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Implementaciones
{
    public class PmTareaTecnicoService: IPmTareaTecnicoService
    {
        private readonly IUnitOfWork _uow;        
        private readonly ILogger<PmTareaTecnico> _logger;

        public PmTareaTecnicoService(IUnitOfWork uow, ILogger<PmTareaTecnico> logger)

        {
            _uow = uow;
            _logger = logger;           
        }

        public async Task<BaseResponse> AddAsync(PmTareaTecnicoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var nuevo = request.Adapt<PmTareaTecnico>();
                await _uow.PmTareaTecnicoRepo.AddAsync(nuevo);

                response.IsSuccess = true;
                response.Message = "PmTareaTecnico registrada exitosamente.";
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al registrar la PmTareaTecnico";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }
        public async Task<BaseResponse> UpdateAsync(int id, PmTareaTecnicoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var PmTareaTecnico = await _uow.PmTareaTecnicoRepo.FindAsync(id);
                if (PmTareaTecnico == null) throw new InvalidDataException("PmTareaTecnico no encontrada");

                request.Adapt(PmTareaTecnico);
                await _uow.SaveAsync();

                response.IsSuccess = true;
                response.Message = "PmTareaTecnico actualizado exitosamente.";
            }
            catch(InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al actualizar la PmTareaTecnico";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }
        public async Task<BaseResponse<PmTareaTecnicoResponse>> FindByIdAsync(int id)
        {
            var response = new BaseResponse<PmTareaTecnicoResponse>();
            try
            {
                var PmTareaTecnico = await _uow.PmTareaTecnicoRepo.FindAsync(id);
                if (PmTareaTecnico == null) throw new InvalidDataException("PmTareaTecnico no encontrada");

                response.IsSuccess = true;
                response.Result = PmTareaTecnico.Adapt<PmTareaTecnicoResponse>();
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al buscar la PmTareaTecnico.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }        
        public async Task<PaginationResponse<ListaPmTareaTecnicoResponse>> ListaAsync(PaginationRequest request)
        {
            var response = new PaginationResponse<ListaPmTareaTecnicoResponse>();
            try
            {
                var result = await _uow.PmTareaTecnicoRepo.ListAsync(
                        predicate: p => p.Status != "Eliminado" && 
                        (string.IsNullOrEmpty(request.Filter)  || p.Descripcion!.Contains(request.Filter) || p.Status.Contains(request.Filter)),
                        selector: p => new ListaPmTareaTecnicoResponse
                        {
                            Id = p.Id,
                            IdPmDetalle = p.IdPmDetalle,
                            IdTecnico = p.IdTecnico,
                            FechaInicialActividad = p.FechaInicialActividad,
                            FechaFinalActividad = p.FechaFinalActividad,
                            DuracionActividad = p.DuracionActividad,
                            Activo = p.Activo,
                            Descripcion = p.Descripcion,
                            TipoActividad = p.IdTipoActividadNavigation.Nombre,
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
                response.Message = "Hubo un error al listar las PmTareaTecnicos.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }
        public async Task<BaseResponse<PmTareaTecnicoResponse>> DeleteAsync(int id)
        {
            var response = new BaseResponse<PmTareaTecnicoResponse>();
            try
            {
                var PmTareaTecnico = await _uow.PmTareaTecnicoRepo.FindAsync(id);
                if (PmTareaTecnico == null) throw new InvalidDataException("PmTareaTecnico no encontrada");

                await _uow.PmTareaTecnicoRepo.DeleteAsync(id);
                response.IsSuccess = true;
                response.Message = "PmTareaTecnico eliminada correctamente.";
                
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al eliminar la PmTareaTecnico.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }
        public async Task<BaseResponse<ICollection<ListaPmTareaTecnicoResponse>>> ListarHistoricoxIdPmDetalle(int idPmDetalle)
        {
            var response = new BaseResponse<ICollection<ListaPmTareaTecnicoResponse>>();
            try
            {
                var result = await _uow.PmTareaTecnicoRepo.ListAsync(
                    predicate: p => p.IdPmDetalle == idPmDetalle && p.Status != "Eliminado",
                    selector: p => new 
                    {
                        p.Id,
                        p.IdTecnico,                        
                        p.IdTipoActividadNavigation.Nombre,
                        p.Descripcion,
                        p.FechaRegistro,
                        p.Status,
                        NombresCompletos = p.IdTecnicoNavigation.Nombres + " " + p.IdTecnicoNavigation.Apellidos,
                    });

                var agrupado = result
                    .GroupBy(x => new { x.Nombre, x.Descripcion, x.FechaRegistro, x.Status })
                    .Select(g => new ListaPmTareaTecnicoResponse
                    {
                        TipoActividad = g.Key.Nombre,
                        Descripcion = g.Key.Descripcion,
                        FechaRegistro = g.Key.FechaRegistro,
                        Status = g.Key.Status,
                        NombresTecnicos = string.Join(", ", g.Select(t => t.NombresCompletos))
                    }).OrderByDescending(x => x.FechaRegistro).ToList();

                response.IsSuccess = true;
                response.Result = agrupado;
            }
            catch (Exception ex)
            {
                response.Message = "Error al obtener el historial.";
                _logger.LogError(ex, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> RegistrarAcciones(PmTareaTecnicoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                //Realizamos las operaciones para PMTareaTecnico
                //Buscamos y actualizamos la ultima actividad en PMTareaTecnico
                var actividadesAbiertas = await _uow.PmTareaTecnicoRepo.FindAllAsync(
                    predicate: x => x.IdPmDetalle == request.IdPmDetalle && x.Activo == true
                );
                foreach (var actividad in actividadesAbiertas)
                {
                    actividad.FechaFinalActividad = DateTime.Now;
                    actividad.Activo = false;
                    var calcularDuracion = FechasDiff.CalcularDuracionMinutos((DateTime)actividad.FechaInicialActividad, (DateTime)actividad.FechaFinalActividad.Value);
                    actividad.DuracionActividad = (decimal)calcularDuracion;
                }               

                //Insertamos la actividad en PMTareaTecnico x cada Tecnico
                var nuevasActividades = request.IdTecnicos.Select(idTecnico => new PmTareaTecnico
                {
                    IdPmDetalle = request.IdPmDetalle,
                    IdTecnico = idTecnico,
                    IdTipoActividad = request.IdTipoActividad,
                    FechaInicialActividad = DateTime.Now,
                    Descripcion = request.Descripcion,
                    Activo = request.IdTipoActividad != 4,
                    FechaFinalActividad = request.IdTipoActividad == 4 ? DateTime.Now : null,
                    DuracionActividad = request.IdTipoActividad == 4 ? 0 : null
                }).ToList();

                await _uow.PmTareaTecnicoRepo.AddRangeAsync(nuevasActividades);

                //Realizamos las operaciones para PMTareaDemora x cada Tecnico (Solo si IdtipoActividad = 2)
                //Buscamos y actualizamos la ultima actividad en PMTareaTecnico
                if(request.IdTipoActividad == 3 || request.IdTipoActividad == 4)
                {
                    var demorasActivas = await _uow.PmTareaDemoraRepo.FindAllAsync(
                            predicate: x => x.IdPmDetalle == request.IdPmDetalle && x.Activo == true
                    );
                    foreach (var demora in demorasActivas)
                    {
                        demora.FechaFinalDemora = DateTime.Now;
                        demora.Activo = false;
                        var calcularDuracion = FechasDiff.CalcularDuracionMinutos((DateTime)demora.FechaInicialDemora, (DateTime)demora.FechaFinalDemora.Value);
                        demora.DuracionDemora = (decimal)calcularDuracion;
                    }
                }

                //Insertamos la actividad en PMTareaTecnico x cada Tecnico
                if (request.IdTipoActividad == 2)
                {
                    var nuevaDemora = request.IdTecnicos.Select(idTecnico => new PmTareaDemora
                    {
                        IdPmDetalle = request.IdPmDetalle,
                        IdTipoDemora = request.IdTipoDemora,
                        FechaInicialDemora = DateTime.Now,
                        Descripcion = request.Descripcion,
                        IdTecnico = idTecnico,
                        Activo = true
                    });
                    await _uow.PmTareaDemoraRepo.AddRangeAsync(nuevaDemora);
                }

                //Actualizamos los datos de la tarea
                var tareaDetalle = await    _uow.PmRepo.GetDetalleTareaPmById(request.IdPmDetalle);
                if (tareaDetalle != null)
                {
                    ActualizarEstadoYFechas(tareaDetalle, request);                    
                }

                await _uow.SaveAsync(); //Este es el punto de todo el cambio de UOW
                response.IsSuccess = true;
                response.Message = "Actividad registrada exitosamente.";
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al registrar la actividad.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }

            return response;            
        }

        private void ActualizarEstadoYFechas(Pmdetalle pmdetalle, PmTareaTecnicoRequest request)
        {
            pmdetalle.Valor1 = request.DatosTarea?.Valor1;
            pmdetalle.Valor2 = request.DatosTarea?.Valor2;
            pmdetalle.Valor3 = request.DatosTarea?.Valor3;
            pmdetalle.Observacion = request.Descripcion;

            int tipo = request.IdTipoActividad;
            if (tipo == 1)
            {
                pmdetalle.StatusTarea = "Procesando";
                pmdetalle.FechaInicialTarea = DateTime.Now;
            }
            else if (tipo == 2) pmdetalle.StatusTarea = "Detenido";
            else if (tipo == 3) pmdetalle.StatusTarea = "Procesando";
            else if (tipo == 4)
            {
                pmdetalle.StatusTarea = "Completado";
                pmdetalle.FechaFinalTarea = DateTime.Now;
                var calcularDuracion  = FechasDiff.CalcularDuracionMinutos(pmdetalle.FechaInicialTarea!.Value, pmdetalle.FechaFinalTarea.Value);
                pmdetalle.DuracionTarea = (decimal)calcularDuracion;

                pmdetalle.Realizado = true;
                pmdetalle.FechaActualizacion = DateTime.Now;
            } 
        }
    }
}
