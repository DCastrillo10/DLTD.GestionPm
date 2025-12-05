using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.TipoHallazgo;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.TipoHallazgo;

namespace DLTD.GestionPm.UI.Proxies.Interfaces
{
    public interface ITipoHallazgoProxy
    {
        Task<PaginationResponse<ListaTipoHallazgoResponse>> Listar(PaginationRequest request);
        Task<BaseResponse<ICollection<ListaTipoHallazgoResponse>>> Listarcombo();
        Task<BaseResponse> Registrar(TipoHallazgoRequest request);
        Task<BaseResponse> Actualizar(int id, TipoHallazgoRequest request);
        Task<BaseResponse> Eliminar(int id);
    }
}
