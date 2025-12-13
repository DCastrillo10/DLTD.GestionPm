using DLTD.GestionPm.Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Request.TipoDemora
{
    public class TipoDemoraRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string Descripcion { get; set; } = default!;

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string Nombre { get; set; } = default!;
        public bool? AplicaDetencion { get; set; } = false;

        public string Status { get; set; } = "Activo";
    }
}
