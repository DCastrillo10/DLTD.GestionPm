using DLTD.GestionPm.Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Request.TipoHallazgo
{
    public class TipoHallazgoRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string Descripcion { get; set; } = default!;

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string Nombre { get; set; } = default!;

        public string Status { get; set; } = "Activo";
    }
}
