using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.AccesoDatos.Configuracion
{
    public class SecurityEntity: IdentityUser
    {
        public int? IdUsuario { get; set; }
        public string? Codigo { get; set; }
        public string? NombresCompletos { get; set; }
    }
}
