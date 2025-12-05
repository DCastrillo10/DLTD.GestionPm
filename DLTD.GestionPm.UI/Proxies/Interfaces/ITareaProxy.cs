using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Tarea;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Tarea;

namespace DLTD.GestionPm.UI.Proxies.Interfaces
{
    public interface ITareaProxy
    {
        Task<PaginationResponse<ListaTareaResponse>> Listar(PaginationRequest request);
        Task<BaseResponse<TareaResponse>> ObtenerPorId(int id);
        Task<BaseResponse> Registrar(TareaRequest request);
        Task<BaseResponse> Actualizar(int id, TareaRequest request);
        Task<BaseResponse> Eliminar(int id);
    }
}
