using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Request.Pm
{
    public class PmDetallesRequest
    {
        public bool Seleccion { get; set; } = false;
        public int Id { get; set; }
        public int IdPm { get; set; }

        public int IdRuta { get; set; }
        public string NomRuta { get; set; } = string.Empty;
        public int IdTarea { get; set; }
        public string NomTarea { get; set; } = string.Empty;

        public DateTime? FechaInicialTarea { get; set; }

        public DateTime? FechaFinalTarea { get; set; }
        
        public decimal? DuracionTarea { get; set; }
        
        public string? StatusTarea { get; set; }

        public bool? Realizado { get; set; }

        public decimal? Valor1 { get; set; }

        public decimal? Valor2 { get; set; }

        public decimal? Valor3 { get; set; }

        public string? Observacion { get; set; }
        
        public decimal? Duracion { get; set; }
        
        public int? NoTecnicos { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public string Status { get; set; } = "Activo";
    }
}
