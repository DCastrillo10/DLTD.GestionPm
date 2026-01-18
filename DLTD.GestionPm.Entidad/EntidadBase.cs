using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Entidad
{
    public class EntidadBase
    {
        public int Id { get; set; }
        public string Status { get; set; } = "Activo";

        public string UsuarioRegistro { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; } 
    }
}
