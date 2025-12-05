using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.TipoDemora;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.TipoDemora;

namespace DLTD.GestionPm.UI.Proxies.Interfaces
{
    public interface ITipoDemoraProxy
    {
        Task<PaginationResponse<ListaTipoDemoraResponse>> Listar(PaginationRequest request);
        Task<BaseResponse<ICollection<ListaTipoDemoraResponse>>> Listarcombo();
        Task<BaseResponse> Registrar(TipoDemoraRequest request);
        Task<BaseResponse> Actualizar(int id, TipoDemoraRequest request);
        Task<BaseResponse> Eliminar(int id);
    }
}
