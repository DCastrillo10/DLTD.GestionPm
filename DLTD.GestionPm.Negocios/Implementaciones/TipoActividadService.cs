using Azure.Core;
using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.TipoActividad;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.TipoActividad;
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
    public class TipoActividadService: ITipoActividadService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<TipoActividad> _logger;
            
        public TipoActividadService(IUnitOfWork uow, ILogger<TipoActividad> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<BaseResponse> AddAsync(TipoActividadRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var nuevo = request.Adapt<TipoActividad>();
                await _uow.TipoActividadRepo.AddAsync(nuevo);

                response.IsSuccess = true;
                response.Message = "Registro realizado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al realizar el registro.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> UpdateAsync(int id, TipoActividadRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var TipoActividad = await _uow.TipoActividadRepo.FindAsync(id);
                if (TipoActividad == null) throw new InvalidDataException("Registro no encontrada");

                request.Adapt(TipoActividad);
                await _uow.SaveAsync();

                response.IsSuccess = true;
                response.Message = "Registro actualizado exitosamente.";
            }
            catch(InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al actualizar el registro";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<TipoActividadResponse>> FindByIdAsync(int id)
        {
            var response = new BaseResponse<TipoActividadResponse>();
            try
            {
                var TipoActividad = await _uow.TipoActividadRepo.FindAsync(id);
                if (TipoActividad == null) throw new InvalidDataException("Registro no encontrado.");

                response.IsSuccess = true;
                response.Result = TipoActividad.Adapt<TipoActividadResponse>();
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al buscar el registro.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<ICollection<ListaTipoActividadResponse>>> ListaSelectAsync()
        {
            var response = new BaseResponse<ICollection<ListaTipoActividadResponse>>();
            try
            {
                var result = await _uow.TipoActividadRepo.ListAsync();
                response.IsSuccess = true;
                response.Result = result.Adapt<ICollection<ListaTipoActividadResponse>>();
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al listar los registros.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }
        public async Task<PaginationResponse<ListaTipoActividadResponse>> ListaAsync(PaginationRequest request)
        {
            var response = new PaginationResponse<ListaTipoActividadResponse>();
            try
            {
                var result = await _uow.TipoActividadRepo.ListAsync(
                        predicate: p => p.Status != "Eliminado" &&
                        (string.IsNullOrEmpty(request.Filter) || p.Nombre.Contains(request.Filter) || p.Descripcion.Contains(request.Filter) || p.Status.Contains(request.Filter)),
                        selector: p => new ListaTipoActividadResponse
                        {
                            Id = p.Id,
                            Nombre = p.Nombre,
                            Descripcion = p.Descripcion,
                            Status = p.Status
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
                response.Message = "Hubo un error al listar los registros.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<TipoActividadResponse>> DeleteAsync(int id)
        {
            var response = new BaseResponse<TipoActividadResponse>();
            try
            {
                var TipoActividad = await _uow.TipoActividadRepo.FindAsync(id);
                if (TipoActividad == null) throw new InvalidDataException("Registro no encontrado.");

                await _uow.TipoActividadRepo.DeleteAsync(id);
                response.IsSuccess = true;
                response.Message = "Registro eliminada correctamente.";
                
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al eliminar el registro.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }
    }
}
