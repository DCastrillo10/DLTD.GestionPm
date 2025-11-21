using DLTD.GestionPm.Dto.Request.Login;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Login;

namespace DLTD.GestionPm.UI.Proxies.Interfaces
{
    public interface ISecurityProxy
    {
        Task<BaseResponse<LoginResponse>> Login(LoginRequest request);
    }
}
