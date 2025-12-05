using Azure.Core;
using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.TipoHallazgo;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.TipoHallazgo;
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
    public class TipoHallazgoService: ITipoHallazgoService
    {
        private readonly ITipoHallazgoRepository _repository;
        private readonly ILogger<TipoHallazgo> _logger;
            
        public TipoHallazgoService(ITipoHallazgoRepository repository, ILogger<TipoHallazgo> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<BaseResponse> AddAsync(TipoHallazgoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var nuevo = request.Adapt<TipoHallazgo>();
                await _repository.AddAsync(nuevo);
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

        public async Task<BaseResponse> UpdateAsync(int id, TipoHallazgoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var TipoHallazgo = await _repository.FindAsync(id);
                if (TipoHallazgo == null) throw new InvalidDataException("Registro no encontrado.");

                request.Adapt(TipoHallazgo);
                await _repository.UpdateAsync();
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
                response.Message = "Hubo un error al actualizar el registro.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<TipoHallazgoResponse>> FindByIdAsync(int id)
        {
            var response = new BaseResponse<TipoHallazgoResponse>();
            try
            {
                var TipoHallazgo = await _repository.FindAsync(id);
                if (TipoHallazgo == null) throw new InvalidDataException("Registro no encontrado.");

                response.IsSuccess = true;
                response.Result = TipoHallazgo.Adapt<TipoHallazgoResponse>();
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

        public async Task<BaseResponse<ICollection<ListaTipoHallazgoResponse>>> ListaSelectAsync()
        {
            var response = new BaseResponse<ICollection<ListaTipoHallazgoResponse>>();
            try
            {
                var result = await _repository.ListAsync();
                response.IsSuccess = true;
                response.Result = result.Adapt<ICollection<ListaTipoHallazgoResponse>>();
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al listar los registros.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }
        public async Task<PaginationResponse<ListaTipoHallazgoResponse>> ListaAsync(PaginationRequest request)
        {
            var response = new PaginationResponse<ListaTipoHallazgoResponse>();
            try
            {
                var result = await _repository.ListAsync(
                        predicate: p => p.Status != "Eliminado" &&
                        (string.IsNullOrEmpty(request.Filter) || p.Nombre.Contains(request.Filter) || p.Descripcion.Contains(request.Filter) || p.Status.Contains(request.Filter)),
                        selector: p => new ListaTipoHallazgoResponse
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

        public async Task<BaseResponse<TipoHallazgoResponse>> DeleteAsync(int id)
        {
            var response = new BaseResponse<TipoHallazgoResponse>();
            try
            {
                var TipoHallazgo = await _repository.FindAsync(id);
                if (TipoHallazgo == null) throw new InvalidDataException("Registro no encontrado.");

                await _repository.DeleteAsync(id);
                response.IsSuccess = true;
                response.Message = "Registro eliminado correctamente.";
                
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
