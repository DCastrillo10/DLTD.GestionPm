using Azure.Core;
using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Marca;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Marca;
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
    public class MarcaService: IMarcaService
    {
        private readonly IMarcaRepository _repository;
        private readonly ILogger<Marca> _logger;

        public MarcaService(IMarcaRepository repository, ILogger<Marca> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<BaseResponse> AddAsync(MarcaRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var nuevo = request.Adapt<Marca>();
                await _repository.AddAsync(nuevo);
                response.IsSuccess = true;
                response.Message = "Marca registrada exitosamente.";
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al registrar la marca";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> UpdateAsync(int id, MarcaRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var marca = await _repository.FindAsync(id);
                if (marca == null) throw new InvalidDataException("Marca no encontrada");

                request.Adapt(marca);
                await _repository.UpdateAsync();
                response.IsSuccess = true;
                response.Message = "Marca actualizado exitosamente.";
            }
            catch(InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al actualizar la marca";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<MarcaResponse>> FindByIdAsync(int id)
        {
            var response = new BaseResponse<MarcaResponse>();
            try
            {
                var marca = await _repository.FindAsync(id);
                if (marca == null) throw new InvalidDataException("Marca no encontrada");

                response.IsSuccess = true;
                response.Result = marca.Adapt<MarcaResponse>();
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

        public async Task<PaginationResponse<ListaMarcaResponse>> ListaAsync(PaginationRequest request)
        {
            var response = new PaginationResponse<ListaMarcaResponse>();
            try
            {
                var result = await _repository.ListAsync(
                        predicate: p => p.Status != "Eliminado" &&
                        (string.IsNullOrEmpty(request.Filter) || p.Descripcion.Contains(request.Filter) || p.Status.Contains(request.Filter)),
                        selector: p => new ListaMarcaResponse
                        {
                            Id = p.Id,
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
                response.Message = "Hubo un error al listar las marcas.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<MarcaResponse>> DeleteAsync(int id)
        {
            var response = new BaseResponse<MarcaResponse>();
            try
            {
                var marca = await _repository.FindAsync(id);
                if (marca == null) throw new InvalidDataException("Marca no encontrada");

                await _repository.DeleteAsync(id);
                response.IsSuccess = true;
                response.Message = "Marca eliminada correctamente.";
                
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
