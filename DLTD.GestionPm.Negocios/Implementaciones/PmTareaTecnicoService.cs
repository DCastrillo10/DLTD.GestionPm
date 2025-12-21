using Azure.Core;
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
        private readonly IPmTareaTecnicoRepository _repository;
        private readonly ILogger<PmTareaTecnico> _logger;

        public PmTareaTecnicoService(IPmTareaTecnicoRepository repository, ILogger<PmTareaTecnico> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<BaseResponse> AddAsync(PmTareaTecnicoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var nuevo = request.Adapt<PmTareaTecnico>();
                await _repository.AddAsync(nuevo);
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
                var PmTareaTecnico = await _repository.FindAsync(id);
                if (PmTareaTecnico == null) throw new InvalidDataException("PmTareaTecnico no encontrada");

                request.Adapt(PmTareaTecnico);
                await _repository.UpdateAsync();
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
                var PmTareaTecnico = await _repository.FindAsync(id);
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
                var result = await _repository.ListAsync(
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
                var PmTareaTecnico = await _repository.FindAsync(id);
                if (PmTareaTecnico == null) throw new InvalidDataException("PmTareaTecnico no encontrada");

                await _repository.DeleteAsync(id);
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
    }
}
