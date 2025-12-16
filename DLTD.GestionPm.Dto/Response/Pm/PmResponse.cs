using DLTD.GestionPm.Comun;
using DLTD.GestionPm.Dto.Request.Pm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Response.Pm
{
    public class PmResponse
    {
        public int Id { get; set; }        
        public int IdTipoPm { get; set; }       
        public int IdModelo { get; set; }      
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

        //Detalles
        public List<PmDetallesResponse> PmDetalles { get; set; } = new();
    }
}
