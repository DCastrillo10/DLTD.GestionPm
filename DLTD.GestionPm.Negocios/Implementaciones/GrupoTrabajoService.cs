using Azure.Core;
using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.GrupoTrabajo;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.GrupoTrabajo;
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
    public class GrupoTrabajoService: IGrupoTrabajoService
    {
        private readonly IGrupoTrabajoRepository _repository;
        private readonly ILogger<GrupoTrabajo> _logger;

        public GrupoTrabajoService(IGrupoTrabajoRepository repository, ILogger<GrupoTrabajo> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<BaseResponse> AddAsync(GrupoTrabajoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var nuevo = request.Adapt<GrupoTrabajo>();
                await _repository.AddAsync(nuevo);
                response.IsSuccess = true;
                response.Message = "Tecnico vinculado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al vincular el tecnico.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }             
        
        public async Task<PaginationResponse<ListaGrupoTrabajoResponse>> ListaAsync(PaginationRequest request)
        {
            var response = new PaginationResponse<ListaGrupoTrabajoResponse>();
            try
            {
                var result = await _repository.ListAsync(
                        predicate: p => p.Status != "Eliminado" &&
                        (string.IsNullOrEmpty(request.Filter) || p.IdTecnicoVinculadoNavigation.Nombres.Contains(request.Filter)  ||
                         p.IdTecnicoVinculadoNavigation.Apellidos.Contains(request.Filter)),
                        selector: p => new ListaGrupoTrabajoResponse
                        {
                            Id = p.Id,
                            IdTecnicoPrincipal = p.IdTecnicoPrincipal,
                            IdTecnicoVinculado = p.IdTecnicoVinculado,
                            NoIdentificacion = p.IdTecnicoVinculadoNavigation.NoIdentificacion,
                            Codigo = p.IdTecnicoVinculadoNavigation.Codigo,
                            Nombres = p.IdTecnicoVinculadoNavigation.Nombres,
                            Apellidos = p.IdTecnicoVinculadoNavigation.Apellidos,
                            FechaVinculacion = p.FechaVinculacion
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
                response.Message = "Hubo un error al listar las GrupoTrabajos.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<GrupoTrabajoResponse>> DeleteAsync(int id)
        {
            var response = new BaseResponse<GrupoTrabajoResponse>();
            try
            {
                var GrupoTrabajo = await _repository.FindAsync(id);
                if (GrupoTrabajo == null) throw new InvalidDataException("GrupoTrabajo no encontrada");

                await _repository.DeleteAsync(id);
                response.IsSuccess = true;
                response.Message = "GrupoTrabajo eliminada correctamente.";
                
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al eliminar la GrupoTrabajo.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }
    }
}
