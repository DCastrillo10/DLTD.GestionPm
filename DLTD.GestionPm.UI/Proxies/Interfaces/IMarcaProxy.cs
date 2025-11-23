using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Marca;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Marca;

namespace DLTD.GestionPm.UI.Proxies.Interfaces
{
    public interface IMarcaProxy
    {
        Task<PaginationResponse<ListaMarcaResponse>> Listar(PaginationRequest request);
        Task<BaseResponse> Registrar(MarcaRequest request);
        Task<BaseResponse> Actualizar(int id, MarcaRequest request);
        Task<BaseResponse> Eliminar(int id);
    }
}
