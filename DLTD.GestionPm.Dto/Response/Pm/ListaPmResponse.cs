using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Response.Pm
{
    public class ListaPmResponse
    {
        public int Id { get; set; }
        public string TipoPm { get; set; } = default!;
        public string Modelo { get; set; } = default!;
        public string WorkOrder { get; set; } = null!;
        public string NoEquipo { get; set; } = null!;
        public string? NoHangar { get; set; }
        public decimal? HorometroActual { get; set; }
        public decimal? HorometroPrevio { get; set; }
        public DateTime? FechaInicialPm { get; set; }
        public DateTime? FechaFinalPm { get; set; }
        public decimal? Duracion { get; set; }
        public string? StatusPm { get; set; }
        public string? Observacion { get; set; }
        public string Status { get; set; } = default!;
    }
}
