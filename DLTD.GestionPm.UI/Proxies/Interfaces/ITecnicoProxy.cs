using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Login;
using DLTD.GestionPm.Dto.Request.Tecnico;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Login;
using DLTD.GestionPm.Dto.Response.Tecnico;

namespace DLTD.GestionPm.UI.Proxies.Interfaces
{
    public interface ITecnicoProxy
    {
        Task<PaginationResponse<ListaTecnicoResponse>> Listar(PaginationRequest request);
        Task<BaseResponse<TecnicoResponse>> ObtenerPorId(int id);
        Task<BaseResponse> Registrar(TecnicoRequest request);
        Task<BaseResponse> Actualizar(int id, TecnicoRequest request);
        Task<BaseResponse> Eliminar(int id);
    }
}
