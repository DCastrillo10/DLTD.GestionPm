using DLTD.GestionPm.Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Request.Tarea
{
    public class TareaRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string CodigoTarea { get; set; } = null!;

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string Descripcion { get; set; } = null!;

        [Range(0.1, double.MaxValue, ErrorMessage ="La duracion debe ser mayor a 0")]
        public decimal Duracion { get; set; }

        [Range(1, int.MaxValue, ErrorMessage ="Debe haber al menos 1 tecnico")]
        public int NoTecnicos { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = Constantes.ErrorSelect)]
        public int IdRuta { get; set; }

        public string Status { get; set; } = "Activo";

    }
}
