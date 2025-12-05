using DLTD.GestionPm.Dto.Request;
using DLTD.GestionPm.Dto.Request.Modelo;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Modelo;

namespace DLTD.GestionPm.UI.Proxies.Interfaces
{
    public interface IModeloProxy
    {
        Task<PaginationResponse<ListaModeloResponse>> Listar(PaginationRequest request);
        Task<BaseResponse<ICollection<ListaModeloResponse>>> ListarCombo();
        Task<BaseResponse<ModeloResponse>> ObtenerPorId(int id);
        Task<BaseResponse> Registrar(ModeloRequest request);
        Task<BaseResponse> Actualizar(int id, ModeloRequest request);
        Task<BaseResponse> Eliminar(int id);
    }
}
