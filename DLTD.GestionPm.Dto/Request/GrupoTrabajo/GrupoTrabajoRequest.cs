using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Request.GrupoTrabajo
{
    public class GrupoTrabajoRequest
    {
        public int Id { get; set; }
        public int IdTecnicoPrincipal { get; set; }

        public int IdTecnicoVinculado { get; set; }

        public DateTime? FechaVinculacion { get; set; }
    }
}
