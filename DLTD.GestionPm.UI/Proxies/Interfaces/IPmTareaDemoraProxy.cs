using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.PmTareaDemora;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.PmTareaDemora;

namespace DLTD.GestionPm.UI.Proxies.Interfaces
{
    public interface IPmTareaDemoraProxy
    {
        Task<PaginationResponse<ListaPmTareaDemoraResponse>> Listar(PaginationRequest request);
        Task<BaseResponse<PmTareaDemoraResponse>> ObtenerPorId(int id);
        Task<BaseResponse> Registrar(PmTareaDemoraRequest request);
        Task<BaseResponse> Actualizar(int id, PmTareaDemoraRequest request);
        Task<BaseResponse> Eliminar(int id);
        Task<BaseResponse<ICollection<PmTareaDemoraResponse>>> HistorialActividades(int id);
        
    }
}
