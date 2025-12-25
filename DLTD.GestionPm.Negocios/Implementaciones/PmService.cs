using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Pm;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Pm;
using DLTD.GestionPm.Entidad;
using DLTD.GestionPm.Negocios.Interfaces;
using DLTD.GestionPm.Repositorios.Interfaces;
using Mapster;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Implementaciones
{
    public class PmService : IPmService
    {
        private readonly IUnitOfWork _uow;
        
        private readonly ILogger<PmService> _logger;

        public PmService(IUnitOfWork uow, ILogger<PmService> logger)
        {            
            _uow = uow;            
            _logger = logger;
        }


        public async Task<BaseResponse> AddMasterDetailsAsync(PmRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var nuevoMasterDetail = request.Adapt<Pm>();
                nuevoMasterDetail.PmDetalles = request.PmDetalles
                                                        .Select(d => d.Adapt<Pmdetalle>())
                                                        .ToList();
                await _uow.PmRepo.AddMasterDetailsAsync(nuevoMasterDetail);
                response.IsSuccess = true;
                response.Message = "Pm registrada exitosamente.";
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al registrar el Pm";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }        

        public async Task<BaseResponse<PmResponse>> FindByIdAsync(int id)
        {
            var response = new BaseResponse<PmResponse>();
            try
            {
                var Pm = await _uow.PmRepo.FindByIdWithDetailAsync(id);
                if (Pm == null) throw new InvalidDataException("Pm no encontrada.");

                var masterDetails = Pm.Adapt<PmResponse>();
                if(Pm.PmDetalles != null)
                {
                    masterDetails.PmDetalles = Pm.PmDetalles
                                 .Where(d => d.Status != "Eliminado")
                                 .Select(d => MapearDetalleConRutas(d))
                                 .ToList();                   

                }
                response.IsSuccess = true;
                response.Result = masterDetails;
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al buscar el Pm.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<PaginationResponse<ListaPmResponse>> ListaAsync(PaginationRequest request)
        {
            var response = new PaginationResponse<ListaPmResponse>();
            try
            {
                var result = await _uow.PmRepo.ListAsync(
                        predicate: p => p.Status != "Eliminado" &&
                                        (string.IsNullOrEmpty(request.Filter) ||
                                        p.NoEquipo!.Contains(request.Filter) ||
                                        p.WorkOrder!.Contains(request.Filter) ||
                                        p.IdTipoPmNavigation.Nombre.Contains(request.Filter) ||
                                        p.IdModeloNavigation.Referencia.Contains(request.Filter) ||
                                        p.Status.Contains(request.Filter)),
                        selector: p => new ListaPmResponse
                        {
                            Id = p.Id,                             
                            TipoPm = p.IdTipoPmNavigation.Nombre ?? "",
                            Modelo = p.IdModeloNavigation.Referencia ?? "",
                            WorkOrder = p.WorkOrder ?? "",
                            NoEquipo = p.NoEquipo ?? "",
                            NoHangar = p.NoHangar ?? "",
                            HorometroActual = p.HorometroActual ?? 0,
                            HorometroPrevio = p.HorometroPrevio ?? 0,
                            FechaInicialPm = p.FechaInicialPm,
                            FechaFinalPm = p.FechaFinalPm,
                            Duracion = p.Duracion ?? 0,
                            StatusPm = p.StatusPm ?? "",
                            Observacion = p.Observacion ?? "",
                            Status = p.Status ?? ""
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
                response.Message = "Hubo un error al listar los Pm.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> UpdateMasterDetailsAsync(int id, PmRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var masterExistente = await _uow.PmRepo.FindByIdWithDetailAsync(id);
                if (masterExistente == null) throw new InvalidDataException("Pm no encontrada.");

                request.Adapt(masterExistente); //Mapear Cabecera: Actualizar las propiedades del Maestro
                ActualizarDetalles(masterExistente, request.PmDetalles); //Actualizamos los detalles
                await _uow.SaveAsync();

                response.IsSuccess = true;
                response.Message = "Pm actualizado exitosamente.";
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al buscar el Pm.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }        

        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                var Pm = await _uow.PmRepo.FindByIdWithDetailAsync(id);
                if (Pm == null) throw new InvalidDataException("Pm no encontrada.");

                Pm.Status = "Eliminado"; //Eliminar cabecera
                foreach (var detalle in Pm.PmDetalles)
                {
                    detalle.Status = "Eliminado";
                }

                await _uow.SaveAsync();
                response.IsSuccess = true;
                response.Message = "Pm eliminado correctamente.";
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al eliminar el Pm.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<bool>> ExistePm(int idTipopm, int idModelo, string NoEquipo, string WorkOrder)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var existe = await _uow.PmRepo.FindPm(idTipopm, idModelo, NoEquipo, WorkOrder);
                if (existe)
                {
                    response.IsSuccess = true;
                    response.Result = true;
                    response.Message = "El Pm ya existe.";
                }
                else
                {
                    response.IsSuccess = true;
                    response.Result = false;
                    response.Message = "El Pm no existe.";
                }               
                
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al verficar el Pm.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<IEnumerable<PmDetallesResponse>>> FindTareas(int idTipoPm, int idModelo)
        {
            var response = new BaseResponse<IEnumerable<PmDetallesResponse>>();
            try
            {
                var tareas = await _uow.PmRepo.FindTareas(idTipoPm, idModelo);
                if(tareas == null || !tareas.Any())
                {
                    response.IsSuccess = true;
                    response.Message = "No hay tareas asociadas al tipo de PM y modelo";
                    response.Result = Enumerable.Empty<PmDetallesResponse>();
                    return response;
                }
                //Mapeamos las entidades de Dominio a los DTOs de Respuesta
                response.Result = tareas.Select(t => new PmDetallesResponse
                {
                    IdTarea = t.Id,
                    NomTarea = t.Descripcion,
                    IdRuta = t.IdRuta,
                    NomRuta = t.IdRutaNavigation.Nombre,
                    Duracion =t.Duracion,
                    NoTecnicos = t.NoTecnicos,
                    Valor1 = 0,
                    Valor2 = 0,
                    Valor3 = 0
                }).ToList();

                response.IsSuccess = true;
                response.Message = "Tareas cargadas exitosamente";
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al cargar las tareas.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<PmDetallesResponse>> GetDetalleTareaPmById(int id) //Permite buscar los datos de un detail por id. (Sin cabecera)
        {
            var response = new BaseResponse<PmDetallesResponse>();
            try
            {
                var detalleTarea = await _uow.PmRepo.GetDetalleTareaPmById(id);
                if (detalleTarea == null) throw new InvalidDataException("Tarea no encontrada.");

                var details = MapearDetalleConRutas(detalleTarea);

                response.IsSuccess = true;
                response.Result = details;
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al buscar la tarea.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> UpdateDetailsById(int id, PmDetallesRequest request) //Permite actualizar los datos de un detail por id. (Sin cabecera)
        {
            var response = new BaseResponse();
            try
            {
                var detalle = await _uow.PmRepo.GetDetalleTareaPmById(id);
                if(detalle == null) return new BaseResponse { Message ="Tarea no existe."};

                request.Adapt(detalle);
                await _uow.SaveAsync();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al actualizar los datos.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }


        //Metodos Privados
        private void ActualizarDetalles(Pm masterExistente, List<PmDetallesRequest> nuevoDetalles) //Permite insertar/eliminar los filas o registros de un masterDetails
        {
            //Manejo de Eliminados

            // Lista de IDs de los registros del detalle que el cliente NOS ENVIÓ
            var idsRequest = nuevoDetalles.Where(d => d.Id > 0).Select(d => d.Id).ToList();
            // Recorrer los registros que actualmente tiene la entidad (los que cargó el FindByIdWithDetailAsync)
            foreach (var detalleExistente in masterExistente.PmDetalles.ToList())
            {
                // Si el registro del detalle cliente NO está en la lista de IDs del Request, fue eliminado por el usuario.
                if (!idsRequest.Contains(detalleExistente.Id))
                {
                    detalleExistente.Status = "Eliminado";
                    //masterExistente.PmPmDetalles.Remove(detalleExistente);
                }
            }

            //Manejo de Agregados y modificados
            foreach (var detalleRequest in nuevoDetalles)
            {
                if (detalleRequest.Id == 0)
                {
                    var nuevoDetalle = detalleRequest.Adapt<Pmdetalle>();
                    //Añadir a la colección rastreada. EF Core le asignará el FK IdPmPm y el estado 'Added'.
                    masterExistente.PmDetalles.Add(nuevoDetalle);
                }
                else
                {
                    var detalleModificar = masterExistente.PmDetalles.FirstOrDefault(d => d.Id == detalleRequest.Id);
                    if (detalleModificar != null)
                    {
                        detalleRequest.Adapt(detalleModificar);
                    }
                }
            }

        } 

        private PmDetallesResponse MapearDetalleConRutas(Pmdetalle entity) //Permite mapear un Detail incluyendo los campos de los FK
        {
            // 1. Mapeo automático de campos 1:1 con Mapster
            var response = entity.Adapt<PmDetallesResponse>();            
            if (entity.IdTareaNavigation != null)
            {                
                response.NomTarea = entity.IdTareaNavigation.Descripcion;
                response.NomRuta = entity.IdTareaNavigation.IdRutaNavigation.Nombre;
            }
            //Aqui puedo colocar otras propiedades de navegacion
            
            return response;
        } 

    }
}
