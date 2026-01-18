using DLTD.GestionPm.Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Request.Pm
{
    public class PmRequest
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = Constantes.ErrorSelect)]
        public int IdTipoPm { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = Constantes.ErrorSelect)]
        public int IdModelo { get; set; }

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string WorkOrder { get; set; } = null!;

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string NoEquipo { get; set; } = null!;

        public string? NoHangar { get; set; }

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public decimal? HorometroActual { get; set; }

        
        public decimal? HorometroPrevio { get; set; } = 0;

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public DateTime? FechaInicialPm { get; set; }

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public DateTime? FechaFinalPm { get; set; }

        [Range(0.01, 999999.00, ErrorMessage ="La duracion debe ser mayor a cero.")]
        public decimal? Duracion { get; set; }

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string? StatusPm { get; set; } = "Pendiente";

        public string? Observacion { get; set; }

        public string Status { get; set; } = "Activo";


        //Detalles y los Ids de los tecnicos o el usuario 
        public List<PmDetallesRequest> PmDetalles { get; set; } = new();

        public List<int> IdTecnicos { get; set; } = new List<int>();

        //Tipo Procesamiento de la tarea Individual o Masivo
        public bool ProcesamientoMasivo { get; set; } = false;

    }
}
