
using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Tarea;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Tarea;
using DLTD.GestionPm.Entidad;
using DLTD.GestionPm.Negocios.Interfaces;
using DLTD.GestionPm.Repositorios.Interfaces;
using Mapster;
using Microsoft.Extensions.Logging;


namespace DLTD.GestionPm.Negocios.Implementaciones
{
    public class TareaService: ITareaService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<TareaService> _logger;

        public TareaService(IUnitOfWork uow, ILogger<TareaService> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<BaseResponse> AddAsync(TareaRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var nuevo = request.Adapt<Tarea>();
                await _uow.TareaRepo.AddAsync(nuevo);
                await _uow.SaveAsync();

                response.IsSuccess = true;
                response.Message = "Tarea registrada exitosamente.";
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al registrar la Tarea";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> UpdateAsync(int id, TareaRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var Tarea = await _uow.TareaRepo.FindAsync(id);
                if (Tarea == null) throw new InvalidDataException("Tarea no encontrada");

                request.Adapt(Tarea);
                await _uow.SaveAsync();

                response.IsSuccess = true;
                response.Message = "Tarea actualizado exitosamente.";
            }
            catch(InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al actualizar la Tarea";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<TareaResponse>> FindByIdAsync(int id)
        {
            var response = new BaseResponse<TareaResponse>();
            try
            {
                var Tarea = await _uow.TareaRepo.FindAsync(id);
                if (Tarea == null) throw new InvalidDataException("Tarea no encontrada.");

                response.IsSuccess = true;
                response.Result = Tarea.Adapt<TareaResponse>();
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al buscar la Tarea.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<PaginationResponse<ListaTareaResponse>> ListaAsync(PaginationRequest request)
        {
            var response = new PaginationResponse<ListaTareaResponse>();
            try
            {
                var result = await _uow.TareaRepo.ListAsync(
                        predicate: p => p.Status != "Eliminado" &&
                        (string.IsNullOrEmpty(request.Filter) || p.CodigoTarea.Contains(request.Filter) || p.Descripcion!.Contains(request.Filter) ||
                                                                 p.IdRutaNavigation.Descripcion.Contains(request.Filter) || p.Status.Contains(request.Filter)),
                        selector: p => new ListaTareaResponse
                        {
                            Id = p.Id,
                            CodigoTarea = p.CodigoTarea,
                            Descripcion = p.Descripcion,
                            Ruta = p.IdRutaNavigation.Nombre,
                            Duracion = p.Duracion,
                            NoTecnicos = p.NoTecnicos,
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
                response.Message = "Hubo un error al listar las Tareas.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<TareaResponse>> DeleteAsync(int id)
        {
            var response = new BaseResponse<TareaResponse>();
            try
            {
                var Tarea = await _uow.TareaRepo.FindAsync(id);
                if (Tarea == null) throw new InvalidDataException("Tarea no encontrada");

                await _uow.TareaRepo.DeleteAsync(id);
                response.IsSuccess = true;
                response.Message = "Tarea eliminada correctamente.";
                
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al eliminar la Tarea.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }


    }
}
