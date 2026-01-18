using DLTD.GestionPm.Comun;
using System.Security.Claims;

namespace DLTD.GestionPm.API.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IHttpContextAccessor _accessor;

        public UsuarioService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public string? GetUserName()
        {
            return _accessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value
                    ?? _accessor.HttpContext?.User?.Identity?.Name
                    ?? "Sistema";
        }
    }
}
