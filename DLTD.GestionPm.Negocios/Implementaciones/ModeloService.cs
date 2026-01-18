
using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Modelo;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Marca;
using DLTD.GestionPm.Dto.Response.Modelo;
using DLTD.GestionPm.Entidad;
using DLTD.GestionPm.Negocios.Interfaces;
using DLTD.GestionPm.Repositorios.Interfaces;
using Mapster;
using Microsoft.Extensions.Logging;


namespace DLTD.GestionPm.Negocios.Implementaciones
{
    public class ModeloService: IModeloService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<Modelo> _logger;

        public ModeloService(IUnitOfWork uow, ILogger<Modelo> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<BaseResponse> AddAsync(ModeloRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var nuevo = request.Adapt<Modelo>();
                await _uow.ModeloRepo.AddAsync(nuevo);
                await _uow.SaveAsync();

                response.IsSuccess = true;
                response.Message = "Modelo registrada exitosamente.";
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al registrar la Modelo";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> UpdateAsync(int id, ModeloRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var modelo = await _uow.ModeloRepo.FindAsync(id);
                if (modelo == null) throw new InvalidDataException("Modelo no encontrada");

                request.Adapt(modelo);
                await _uow.SaveAsync();

                response.IsSuccess = true;
                response.Message = "Modelo actualizado exitosamente.";
            }
            catch(InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al actualizar la Modelo";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<ModeloResponse>> FindByIdAsync(int id)
        {
            var response = new BaseResponse<ModeloResponse>();
            try
            {
                var modelo = await _uow.ModeloRepo.FindAsync(id);
                if (modelo == null) throw new InvalidDataException("Modelo no encontrada");

                response.IsSuccess = true;
                response.Result = modelo.Adapt<ModeloResponse>();
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al buscar la Modelo.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<ICollection<ListaModeloResponse>>> ListaSelectAsync()
        {
            var response = new BaseResponse<ICollection<ListaModeloResponse>>();
            try
            {
                var result = await _uow.ModeloRepo.ListAsync();
                response.IsSuccess = true;
                response.Result = result.Adapt<ICollection<ListaModeloResponse>>();
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al listar las Rutas.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }
        public async Task<PaginationResponse<ListaModeloResponse>> ListaAsync(PaginationRequest request)
        {
            var response = new PaginationResponse<ListaModeloResponse>();
            try
            {
                var result = await _uow.ModeloRepo.ListAsync(
                        predicate: p => p.Status != "Eliminado" &&
                        (string.IsNullOrEmpty(request.Filter) || p.Referencia.Contains(request.Filter) || p.Descripcion!.Contains(request.Filter) ||
                                                                 p.IdMarcaNavigation.Descripcion.Contains(request.Filter) || p.Status.Contains(request.Filter)),
                        selector: p => new ListaModeloResponse
                        {
                            Id = p.Id,
                            Referencia = p.Referencia,
                            Descripcion = p.Descripcion,
                            Marca = p.IdMarcaNavigation.Nombre,
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
                response.Message = "Hubo un error al listar las Modelos.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<ModeloResponse>> DeleteAsync(int id)
        {
            var response = new BaseResponse<ModeloResponse>();
            try
            {
                var modelo = await _uow.ModeloRepo.FindAsync(id);
                if (modelo == null) throw new InvalidDataException("Modelo no encontrada");

                await _uow.ModeloRepo.DeleteAsync(id);
                response.IsSuccess = true;
                response.Message = "Modelo eliminada correctamente.";
                
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al eliminar la Modelo.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }
    }
}
