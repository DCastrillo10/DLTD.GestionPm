using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Response.Login
{
    public class LoginResponse
    {
        public string Token { get; set; } = default!;
        public string Nombre { get; set; } = default!;
        public string Rol { get; set; } = default!;
    }
}
