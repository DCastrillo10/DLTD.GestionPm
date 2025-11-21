using DLTD.GestionPm.Dto.Request.Login;
using DLTD.GestionPm.Dto.Request.Tecnico;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Login;

namespace DLTD.GestionPm.UI.Proxies.Interfaces
{
    public interface ITecnicoProxy
    {
        Task<BaseResponse> Registrar(TecnicoRequest request);
    }
}
