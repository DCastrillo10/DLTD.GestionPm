using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.PmTareaHallazgo;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.PmTareaHallazgo;

namespace DLTD.GestionPm.UI.Proxies.Interfaces
{
    public interface IPmTareaHallazgoProxy
    {
        Task<PaginationResponse<ListaPmTareaHallazgoResponse>> Listar(PaginationRequest request);
        Task<BaseResponse<PmTareaHallazgoResponse>> ObtenerPorId(int id);
        Task<BaseResponse> Registrar(PmTareaHallazgoRequest request);
        Task<BaseResponse> Actualizar(int id, PmTareaHallazgoRequest request);
        Task<BaseResponse> Eliminar(int id);

        Task<BaseResponse<string>> ObtenerEquipo(int id);


    }
}
