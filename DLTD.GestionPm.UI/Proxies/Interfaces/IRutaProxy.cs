using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Ruta;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Ruta;

namespace DLTD.GestionPm.UI.Proxies.Interfaces
{
    public interface IRutaProxy
    {
        Task<PaginationResponse<ListaRutaResponse>> Listar(PaginationRequest request);
        Task<BaseResponse<ICollection<ListaRutaResponse>>> Listarcombo();
        Task<BaseResponse> Registrar(RutaRequest request);
        Task<BaseResponse> Actualizar(int id, RutaRequest request);
        Task<BaseResponse> Eliminar(int id);
    }
}
