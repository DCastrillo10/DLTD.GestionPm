using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.TipoActividad;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.TipoActividad;

namespace DLTD.GestionPm.UI.Proxies.Interfaces
{
    public interface ITipoActividadProxy
    {
        Task<PaginationResponse<ListaTipoActividadResponse>> Listar(PaginationRequest request);
        Task<BaseResponse<ICollection<ListaTipoActividadResponse>>> Listarcombo();
        Task<BaseResponse> Registrar(TipoActividadRequest request);
        Task<BaseResponse> Actualizar(int id, TipoActividadRequest request);
        Task<BaseResponse> Eliminar(int id);
    }
}
