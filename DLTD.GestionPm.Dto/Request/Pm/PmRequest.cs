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
        public decimal? Horometro { get; set; }

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public DateTime? FechaInicialPm { get; set; }

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public DateTime? FechaFinalPm { get; set; }

        public decimal? Duracion { get; set; }

        
        public string? StatusPm { get; set; }

        public string? Observacion { get; set; }

        public string Status { get; set; } = "Activo";

        //Detalles
        public List<PmDetallesRequest> PmDetalles { get; set; } = new();
    }
}
