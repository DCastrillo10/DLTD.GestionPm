using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Maquina;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Maquina;

namespace DLTD.GestionPm.UI.Proxies.Interfaces
{
    public interface IMaquinaProxy
    {
        Task<PaginationResponse<ListaMaquinaResponse>> Listar(PaginationRequest request);
        Task<BaseResponse<MaquinaResponse>> ObtenerPorId(int id);
        Task<BaseResponse> Registrar(MaquinaRequest request);
        Task<BaseResponse> Actualizar(int id, MaquinaRequest request);
        Task<BaseResponse> Eliminar(int id);
    }
}
