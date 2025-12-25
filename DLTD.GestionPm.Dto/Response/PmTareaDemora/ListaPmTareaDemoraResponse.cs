using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Response.PmTareaDemora
{
    public class ListaPmTareaDemoraResponse
    {
        public int Id { get; set; }
        public int IdPmDetalle { get; set; }

        public int IdTipoDemora { get; set; }

        public DateTime FechaInicialDemora { get; set; }

        public DateTime? FechaFinalDemora { get; set; }

        public decimal? DuracionDemora { get; set; }

        public string? Descripcion { get; set; }

        public int IdTecnico { get; set; }

        public string Status { get; set; } = default!;

        public string? UsuarioRegistro { get; set; }

        public DateTime FechaRegistro { get; set; }

        //nuevas propiedades
        public string? TipoDemora { get; set; }
        public string? NombresTecnicos { get; set; }
    }
}
