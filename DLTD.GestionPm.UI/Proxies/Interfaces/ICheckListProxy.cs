using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.CheckList;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.CheckList;

namespace DLTD.GestionPm.UI.Proxies.Interfaces
{
    public interface ICheckListProxy
    {
        Task<BaseResponse> Registrar(CheckListRequest request);
        Task<BaseResponse> Actualizar(int id, CheckListRequest request);        
        Task<BaseResponse<CheckListResponse>> ObtenerPorId(int id);
        Task<PaginationResponse<ListaCheckListResponse>> Listar(PaginationRequest request);        
        Task<BaseResponse> Eliminar(int id);

        Task<BaseResponse<bool>> ExisteCheckListAsync(int idTipoPm, int idModelo);
    }
}
