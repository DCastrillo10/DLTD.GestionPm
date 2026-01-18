
using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Maquina;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Maquina;
using DLTD.GestionPm.Entidad;
using DLTD.GestionPm.Negocios.Interfaces;
using DLTD.GestionPm.Repositorios.Interfaces;
using Mapster;
using Microsoft.Extensions.Logging;


namespace DLTD.GestionPm.Negocios.Implementaciones
{
    public class MaquinaService: IMaquinaService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<Maquina> _logger;

        public MaquinaService(IUnitOfWork uow, ILogger<Maquina> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<BaseResponse> AddAsync(MaquinaRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var nuevo = request.Adapt<Maquina>();
                await _uow.MaquinaRepo.AddAsync(nuevo);
                await _uow.SaveAsync();

                response.IsSuccess = true;
                response.Message = "Maquina registrada exitosamente.";
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al registrar la Maquina";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> UpdateAsync(int id, MaquinaRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var Maquina = await _uow.MaquinaRepo.FindAsync(id);
                if (Maquina == null) throw new InvalidDataException("Maquina no encontrada");

                request.Adapt(Maquina);
                await _uow.SaveAsync();

                response.IsSuccess = true;
                response.Message = "Maquina actualizado exitosamente.";
            }
            catch(InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al actualizar la Maquina";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<MaquinaResponse>> FindByIdAsync(int id)
        {
            var response = new BaseResponse<MaquinaResponse>();
            try
            {
                var Maquina = await _uow.MaquinaRepo.FindAsync(id);
                if (Maquina == null) throw new InvalidDataException("Maquina no encontrada");

                response.IsSuccess = true;
                response.Result = Maquina.Adapt<MaquinaResponse>();
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al buscar la Maquina.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<PaginationResponse<ListaMaquinaResponse>> ListaAsync(PaginationRequest request)
        {
            var response = new PaginationResponse<ListaMaquinaResponse>();
            try
            {
                var result = await _uow.MaquinaRepo.ListAsync(
                        predicate: p => p.Status != "Eliminado" &&
                        (string.IsNullOrEmpty(request.Filter) || p.Codigo.Contains(request.Filter) || p.Descripcion!.Contains(request.Filter) ||
                                                                 p.IdModeloNavigation.Descripcion!.Contains(request.Filter) || p.Status.Contains(request.Filter)),
                        selector: p => new ListaMaquinaResponse
                        {
                            Id = p.Id,
                            Codigo = p.Codigo,
                            Descripcion = p.Descripcion,
                            Modelo = p.IdModeloNavigation.Descripcion!,
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
                response.Message = "Hubo un error al listar las Maquinas.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<MaquinaResponse>> DeleteAsync(int id)
        {
            var response = new BaseResponse<MaquinaResponse>();
            try
            {
                var Maquina = await _uow.MaquinaRepo.FindAsync(id);
                if (Maquina == null) throw new InvalidDataException("Maquina no encontrada");

                await _uow.MaquinaRepo.DeleteAsync(id);
                response.IsSuccess = true;
                response.Message = "Maquina eliminada correctamente.";
                
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al eliminar la Maquina.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<MaquinaResponse>> FindMaquinaByNoEquipo(string NoEquipo)
        {
            var response = new BaseResponse<MaquinaResponse>();
            try
            {
                var Equipo = await _uow.MaquinaRepo.FindMaquina(NoEquipo);
                if (Equipo == null) throw new InvalidDataException("Maquina no encontrada");

                response.IsSuccess = true;
                response.Result = Equipo.Adapt<MaquinaResponse>();
            }
            catch (InvalidDataException ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al buscar el Equipo.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }
    }
}
