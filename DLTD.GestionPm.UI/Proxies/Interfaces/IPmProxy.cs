using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Pm;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Pm;

namespace DLTD.GestionPm.UI.Proxies.Interfaces
{
    public interface IPmProxy
    {
        Task<BaseResponse> Registrar(PmRequest request);
        Task<BaseResponse> Actualizar(int id, PmRequest request);        
        Task<BaseResponse<PmResponse>> ObtenerPorId(int id);
        Task<PaginationResponse<ListaPmResponse>> Listar(PaginationRequest request);        
        Task<BaseResponse> Eliminar(int id);

        Task<BaseResponse<bool>> ExistePmAsync(int idTipoPm, int idModelo, string NoEquipo, string WorkOrder);

        Task<BaseResponse<IEnumerable<PmDetallesResponse>>>ObtenerTareasxModeloTipoPm(int idTipoPm, int idModelo);
    }
}
