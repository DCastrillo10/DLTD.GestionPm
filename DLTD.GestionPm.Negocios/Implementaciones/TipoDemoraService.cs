using Azure.Core;
using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.TipoDemora;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.TipoDemora;
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
    public class TipoDemoraService: ITipoDemoraService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<TipoDemora> _logger;
            
        public TipoDemoraService(IUnitOfWork uow, ILogger<TipoDemora> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<BaseResponse> AddAsync(TipoDemoraRequest request)
        {
            var response = new BaseResponse();
            try
            {
                request.AplicaDetencion ??= false;
                var nuevo = request.Adapt<TipoDemora>();
                await _uow.TipoDemoraRepo.AddAsync(nuevo);

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

        public async Task<BaseResponse> UpdateAsync(int id, TipoDemoraRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var TipoDemora = await _uow.TipoDemoraRepo.FindAsync(id);
                if (TipoDemora == null) throw new InvalidDataException("Registro no encontrado.");

                request.Adapt(TipoDemora);
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
                response.Message = "Hubo un error al actualizar el registro.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<TipoDemoraResponse>> FindByIdAsync(int id)
        {
            var response = new BaseResponse<TipoDemoraResponse>();
            try
            {
                var TipoDemora = await _uow.TipoDemoraRepo.FindAsync(id);
                if (TipoDemora == null) throw new InvalidDataException("Registro no encontrado.");

                response.IsSuccess = true;
                response.Result = TipoDemora.Adapt<TipoDemoraResponse>();
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

        public async Task<BaseResponse<ICollection<ListaTipoDemoraResponse>>> ListaSelectAsync()
        {
            var response = new BaseResponse<ICollection<ListaTipoDemoraResponse>>();
            try
            {
                var result = await _uow.TipoDemoraRepo.ListAsync();
                response.IsSuccess = true;
                response.Result = result.Adapt<ICollection<ListaTipoDemoraResponse>>();
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al listar los registros.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }
        public async Task<PaginationResponse<ListaTipoDemoraResponse>> ListaAsync(PaginationRequest request)
        {
            var response = new PaginationResponse<ListaTipoDemoraResponse>();
            try
            {
                var result = await _uow.TipoDemoraRepo.ListAsync(
                        predicate: p => p.Status != "Eliminado" &&
                        (string.IsNullOrEmpty(request.Filter) || p.Nombre.Contains(request.Filter) || p.Descripcion.Contains(request.Filter) || p.Status.Contains(request.Filter)),
                        selector: p => new ListaTipoDemoraResponse
                        {
                            Id = p.Id,
                            Nombre = p.Nombre,
                            Descripcion = p.Descripcion,
                            AplicaDetencion = p.AplicaDetencion,
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

        public async Task<BaseResponse<TipoDemoraResponse>> DeleteAsync(int id)
        {
            var response = new BaseResponse<TipoDemoraResponse>();
            try
            {
                var TipoDemora = await _uow.TipoDemoraRepo.FindAsync(id);
                if (TipoDemora == null) throw new InvalidDataException("Registro no encontrado.");

                await _uow.TipoDemoraRepo.DeleteAsync(id);
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
