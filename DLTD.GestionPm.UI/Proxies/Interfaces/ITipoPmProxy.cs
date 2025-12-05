using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.TipoPm;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.TipoPm;

namespace DLTD.GestionPm.UI.Proxies.Interfaces
{
    public interface ITipoPmProxy
    {
        Task<PaginationResponse<ListaTipoPmResponse>> Listar(PaginationRequest request);
        Task<BaseResponse<ICollection<ListaTipoPmResponse>>> Listarcombo();
        Task<BaseResponse> Registrar(TipoPmRequest request);
        Task<BaseResponse> Actualizar(int id, TipoPmRequest request);
        Task<BaseResponse> Eliminar(int id);
    }
}
