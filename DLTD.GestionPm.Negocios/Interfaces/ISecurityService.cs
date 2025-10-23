using DLTD.GestionPm.Dto.Request.Login;
using DLTD.GestionPm.Dto.Response;
using DLTD.GestionPm.Dto.Response.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Negocios.Interfaces
{
    public interface ISecurityService
    {
        Task<BaseResponse<LoginResponse>> Login(LoginRequest request);
    }
}
