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
        public string Status { get; set; } = null!;

        public string UsuarioRegistro { get; set; } = null!;

        public DateTime FechaRegistro { get; set; }
    }
}
