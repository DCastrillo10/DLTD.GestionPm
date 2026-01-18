using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.CheckList;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.CheckList;
using DLTD.GestionPm.Entidad;
using DLTD.GestionPm.Negocios.Interfaces;
using DLTD.GestionPm.Repositorios.Interfaces;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Implementaciones
{
    public class CheckListService : ICheckListService
    {
        private readonly IUnitOfWork _uow;
        
        private readonly ILogger<PmcheckList> _logger;

        public CheckListService(IUnitOfWork uow, ILogger<PmcheckList> logger)
        {            
            _uow = uow;            
            _logger = logger;
        }


        public async Task<BaseResponse> AddMasterDetailsAsync(CheckListRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var nuevoMasterDetail = request.Adapt<PmcheckList>();
                nuevoMasterDetail.PmcheckListDetalles = request.Detalles
                                                        .Select(d => d.Adapt<PmcheckListDetalle>())
                                                        .ToList();
                await _uow.CheckListRepo.AddMasterDetailsAsync(nuevoMasterDetail);
                await _uow.SaveAsync();

                response.IsSuccess = true;
                response.Message = "CheckList registrada exitosamente.";
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al registrar el Checklist";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }        

        public async Task<BaseResponse<CheckListResponse>> FindByIdAsync(int id)
        {
            var response = new BaseResponse<CheckListResponse>();
            try
            {
                var checkList = await _uow.CheckListRepo.FindByIdWithDetailAsync(id);
                if (checkList == null) throw new InvalidDataException("Checklist no encontrada.");

                var masterDetails = checkList.Adapt<CheckListResponse>();
                if(checkList.PmcheckListDetalles != null)
                {
                    masterDetails.Detalles = checkList.PmcheckListDetalles
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
                response.Message = "Hubo un error al buscar el Checklist.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<PaginationResponse<ListaCheckListResponse>> ListaAsync(PaginationRequest request)
        {
            var response = new PaginationResponse<ListaCheckListResponse>();
            try
            {
                var result = await _uow.CheckListRepo.ListAsync(
                        predicate: p => p.Status != "Eliminado" &&
                                        (string.IsNullOrEmpty(request.Filter) ||
                                        p.Descripcion!.Contains(request.Filter) ||
                                        p.IdTipoPmNavigation.Nombre.Contains(request.Filter) ||
                                        p.IdModeloNavigation.Referencia.Contains(request.Filter) ||
                                        p.Status.Contains(request.Filter)),
                        selector: p => new ListaCheckListResponse
                        {
                            Id = p.Id,
                            Descripcion = p.Descripcion ?? "",
                            TipoPm = p.IdTipoPmNavigation.Nombre ?? "",
                            Modelo = p.IdModeloNavigation.Referencia ?? "",
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
                response.Message = "Hubo un error al listar los checklist.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> UpdateMasterDetailsAsync(int id, CheckListRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var masterExistente = await _uow.CheckListRepo.FindByIdWithDetailAsync(id);
                if (masterExistente == null) throw new InvalidDataException("Checklist no encontrada.");

                request.Adapt(masterExistente); //Mapear Cabecera: Actualizar las propiedades del Maestro
                ActualizarDetalles(masterExistente, request.Detalles); //Actualizamos los detalles
                await _uow.SaveAsync();

                response.IsSuccess = true;
                response.Message = "Checklist actualizado exitosamente.";
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al buscar el Checklist.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }        

        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                var checkList = await _uow.CheckListRepo.FindByIdWithDetailAsync(id);
                if (checkList == null) throw new InvalidDataException("Checklist no encontrada.");

                checkList.Status = "Eliminado"; //Eliminar cabecera
                foreach (var detalle in checkList.PmcheckListDetalles)
                {
                    detalle.Status = "Eliminado";
                }

                
                response.IsSuccess = true;
                response.Message = "Checklist eliminado correctamente.";
            }
            catch (InvalidDataException ex)
            {
                response.Message = ex.Message;
                _logger.LogWarning(ex, "{0}: {1}", response.Message, ex.Message);
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Hubo un error al eliminar el Checklist.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<bool>> ExisteChecklist(int idTipopm, int idModelo)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var existe = await _uow.CheckListRepo.FindCheckList(idTipopm, idModelo);
                if (existe)
                {
                    response.IsSuccess = true;
                    response.Result = true;
                    response.Message = "El checklist ya existe.";
                }
                else
                {
                    response.IsSuccess = true;
                    response.Result = false;
                    response.Message = "El checklist no existe.";
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
                response.Message = "Hubo un error al verficar el Checklist.";
                _logger.LogError(ex, "{0}: {1}", response.Message, ex.Message);
            }
            return response;
        }

        private void ActualizarDetalles(PmcheckList masterExistente, List<CheckListDetallesRequest> nuevoDetalles)
        {
            //Manejo de Eliminados

            // Lista de IDs de los registros del detalle que el cliente NOS ENVIÓ
            var idsRequest = nuevoDetalles.Where(d => d.Id > 0).Select(d => d.Id).ToList();
            // Recorrer los registros que actualmente tiene la entidad (los que cargó el FindByIdWithDetailAsync)
            foreach (var detalleExistente in masterExistente.PmcheckListDetalles.ToList())
            {
                // Si el registro del detalle cliente NO está en la lista de IDs del Request, fue eliminado por el usuario.
                if (!idsRequest.Contains(detalleExistente.Id))
                {
                    detalleExistente.Status = "Eliminado";
                    //masterExistente.PmcheckListDetalles.Remove(detalleExistente);
                }
            }

            //Manejo de Agregados y modificados
            foreach (var detalleRequest in nuevoDetalles)
            {
                if (detalleRequest.Id == 0)
                {
                    var nuevoDetalle = detalleRequest.Adapt<PmcheckListDetalle>();
                    //Añadir a la colección rastreada. EF Core le asignará el FK IdPmCheckList y el estado 'Added'.
                    masterExistente.PmcheckListDetalles.Add(nuevoDetalle);
                }
                else
                {
                    var detalleModificar = masterExistente.PmcheckListDetalles.FirstOrDefault(d => d.Id == detalleRequest.Id);
                    if (detalleModificar != null)
                    {
                        detalleRequest.Adapt(detalleModificar);
                    }
                }
            }

        }

        private CheckListDetallesResponse MapearDetalleConRutas(PmcheckListDetalle entity)
        {
            // 1. Mapeo automático de campos 1:1 con Mapster
            var response = entity.Adapt<CheckListDetallesResponse>();            
            if (entity.IdRutaNavigation != null)
            {                
                response.Nombre = entity.IdRutaNavigation.Nombre;
            }
            //Aqui puedo colocar otras propiedades de navegacion
            
            return response;
        }
    }
}
