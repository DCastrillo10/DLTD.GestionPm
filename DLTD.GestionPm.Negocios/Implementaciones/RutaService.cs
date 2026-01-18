using Azure;
using Azure.Core;
using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Ruta;
using DLTD.GestionPm.Dto.Response;
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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Implementaciones
{
    public class RutaService: IRutaService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<Ruta> _logger;

        public RutaService(IUnitOfWork uow, ILogger<Ruta> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<BaseResponse> AddAsync(RutaRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var nuevo = request.Adapt<Ruta>();
                await _uow.RutaRepo.AddAsync(nuevo);
                await _uow.SaveAsync();

                response.IsSuccess = true;
                response.Message = "Ruta registrada exitosamente.";
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al registrar la Ruta";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> UpdateAsync(int id, RutaRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var Ruta = await _uow.RutaRepo.FindAsync(id);
                if (Ruta == null) throw new InvalidDataException("Ruta no encontrada");

                request.Adapt(Ruta);
                await _uow.SaveAsync();

                response.IsSuccess = true;
                response.Message = "Ruta actualizado exitosamente.";
            }
            catch(InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al actualizar la Ruta";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<RutaResponse>> FindByIdAsync(int id)
        {
            var response = new BaseResponse<RutaResponse>();
            try
            {
                var Ruta = await _uow.RutaRepo.FindAsync(id);
                if (Ruta == null) throw new InvalidDataException("Ruta no encontrada");

                response.IsSuccess = true;
                response.Result = Ruta.Adapt<RutaResponse>();
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al buscar la Ruta.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<ICollection<ListaRutaResponse>>> ListaSelectAsync()
        {
            var response = new BaseResponse<ICollection<ListaRutaResponse>>();
            try
            {
                var result = await _uow.RutaRepo.ListAsync();
                response.IsSuccess = true;
                response.Result = result.Adapt<ICollection<ListaRutaResponse>>();
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al listar las Rutas.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }
        public async Task<PaginationResponse<ListaRutaResponse>> ListaAsync(PaginationRequest request)
        {
            var response = new PaginationResponse<ListaRutaResponse>();
            try
            {
                var result = await _uow.RutaRepo.ListAsync(
                        predicate: p => p.Status != "Eliminado" &&
                        (string.IsNullOrEmpty(request.Filter) || p.Nombre.Contains(request.Filter) || p.Descripcion.Contains(request.Filter) || p.Status.Contains(request.Filter)),
                        selector: p => new ListaRutaResponse
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
                response.Message = "Hubo un error al listar las Rutas.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<RutaResponse>> DeleteAsync(int id)
        {
            var response = new BaseResponse<RutaResponse>();
            try
            {
                var Ruta = await _uow.RutaRepo.FindAsync(id);
                if (Ruta == null) throw new InvalidDataException("Ruta no encontrada");

                await _uow.RutaRepo.DeleteAsync(id);
                response.IsSuccess = true;
                response.Message = "Ruta eliminada correctamente.";
                
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al eliminar la Ruta.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }
    }
}
