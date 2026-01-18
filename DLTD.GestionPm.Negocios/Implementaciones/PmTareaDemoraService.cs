using Azure.Core;
using DLTD.GestionPm.Comun;
using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.PmTareaDemora;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.PmTareaDemora;
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
    public class PmTareaDemoraService: IPmTareaDemoraService
    {
        private readonly IUnitOfWork _uow;        
        private readonly ILogger<PmTareaDemora> _logger;

        public PmTareaDemoraService(IUnitOfWork uow, ILogger<PmTareaDemora> logger)
        {
            _uow = uow;
            _logger = logger;            
        }

        public async Task<BaseResponse> AddAsync(PmTareaDemoraRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var nuevo = request.Adapt<PmTareaDemora>();
                await _uow.PmTareaDemoraRepo.AddAsync(nuevo);
                await _uow.SaveAsync();

                response.IsSuccess = true;
                response.Message = "PmTareaDemora registrada exitosamente.";
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al registrar la PmTareaDemora";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }
        public async Task<BaseResponse> UpdateAsync(int id, PmTareaDemoraRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var PmTareaDemora = await _uow.PmTareaDemoraRepo.FindAsync(id);
                if (PmTareaDemora == null) throw new InvalidDataException("PmTareaDemora no encontrada");

                request.Adapt(PmTareaDemora);
                await _uow.SaveAsync();

                response.IsSuccess = true;
                response.Message = "PmTareaDemora actualizado exitosamente.";
            }
            catch(InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al actualizar la PmTareaDemora";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }
        public async Task<BaseResponse<PmTareaDemoraResponse>> FindByIdAsync(int id)
        {
            var response = new BaseResponse<PmTareaDemoraResponse>();
            try
            {
                var PmTareaDemora = await _uow.PmTareaDemoraRepo.FindAsync(id);
                if (PmTareaDemora == null) throw new InvalidDataException("PmTareaDemora no encontrada");

                response.IsSuccess = true;
                response.Result = PmTareaDemora.Adapt<PmTareaDemoraResponse>();
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al buscar la PmTareaDemora.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }        
        public async Task<PaginationResponse<ListaPmTareaDemoraResponse>> ListaAsync(PaginationRequest request)
        {
            var response = new PaginationResponse<ListaPmTareaDemoraResponse>();
            try
            {
                var result = await _uow.PmTareaDemoraRepo.ListAsync(
                        predicate: p => p.Status != "Eliminado" && 
                        (string.IsNullOrEmpty(request.Filter)  || p.Descripcion!.Contains(request.Filter) || p.Status.Contains(request.Filter)),
                        selector: p => new ListaPmTareaDemoraResponse
                        {
                            Id = p.Id,
                            IdPmDetalle = p.IdPmDetalle,
                            IdTipoDemora = p.IdTipoDemora,
                            IdTecnico = p.IdTecnico,
                            FechaInicialDemora = p.FechaInicialDemora,
                            FechaFinalDemora = p.FechaFinalDemora,
                            DuracionDemora = p.DuracionDemora,                            
                            Descripcion = p.Descripcion,
                            TipoDemora = p.IdTipoDemoraNavigation.Nombre,
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
                response.Message = "Hubo un error al listar las PmTareaDemoras.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }
        public async Task<BaseResponse<PmTareaDemoraResponse>> DeleteAsync(int id)
        {
            var response = new BaseResponse<PmTareaDemoraResponse>();
            try
            {
                var PmTareaDemora = await _uow.PmTareaDemoraRepo.FindAsync(id);
                if (PmTareaDemora == null) throw new InvalidDataException("PmTareaDemora no encontrada");

                await _uow.PmTareaDemoraRepo.DeleteAsync(id);
                response.IsSuccess = true;
                response.Message = "PmTareaDemora eliminada correctamente.";
                
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al eliminar la PmTareaDemora.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }
        public async Task<BaseResponse<ICollection<ListaPmTareaDemoraResponse>>> ListarHistoricoxIdPmDetalle(int idPmDetalle)
        {
            var response = new BaseResponse<ICollection<ListaPmTareaDemoraResponse>>();
            try
            {
                var result = await _uow.PmTareaDemoraRepo.ListAsync(
                    predicate: p => p.IdPmDetalle == idPmDetalle && p.Status != "Eliminado",
                    selector: p => new ListaPmTareaDemoraResponse
                    {
                        Id = p.Id,
                        IdTecnico = p.IdTecnico,
                        NombresTecnicos = p.IdTecnicoNavigation.Nombres + " " + p.IdTecnicoNavigation.Apellidos,
                        TipoDemora = p.IdTipoDemoraNavigation.Nombre,
                        Descripcion = p.Descripcion,
                        FechaRegistro = p.FechaRegistro,
                        Status = p.Status
                    });
                response.IsSuccess = true;
                response.Result = result.OrderByDescending(x => x.Id).ToList();
            }
            catch (Exception ex)
            {
                response.Message = "Error al obtener el historial.";
                _logger.LogError(ex, ex.Message);
            }
            return response;
        }

        
    }
}
