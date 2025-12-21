using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.GrupoTrabajo;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.GrupoTrabajo;

namespace DLTD.GestionPm.UI.Proxies.Interfaces
{
    public interface IGrupoTrabajoProxy
    {
        Task<PaginationResponse<ListaGrupoTrabajoResponse>> Listar(PaginationRequest request);        
        Task<BaseResponse> Registrar(GrupoTrabajoRequest request);        
        Task<BaseResponse> Eliminar(int id);
    }
}
