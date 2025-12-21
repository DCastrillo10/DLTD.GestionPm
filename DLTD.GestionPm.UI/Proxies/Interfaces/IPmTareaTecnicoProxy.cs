using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.PmTareaTecnico;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.PmTareaTecnico;

namespace DLTD.GestionPm.UI.Proxies.Interfaces
{
    public interface IPmTareaTecnicoProxy
    {
        Task<PaginationResponse<ListaPmTareaTecnicoResponse>> Listar(PaginationRequest request);
        Task<BaseResponse<PmTareaTecnicoResponse>> ObtenerPorId(int id);
        Task<BaseResponse> Registrar(PmTareaTecnicoRequest request);
        Task<BaseResponse> Actualizar(int id, PmTareaTecnicoRequest request);
        Task<BaseResponse> Eliminar(int id);
    }
}
