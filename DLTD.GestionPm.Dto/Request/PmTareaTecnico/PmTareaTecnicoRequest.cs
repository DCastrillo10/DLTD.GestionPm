using DLTD.GestionPm.Dto.Request.Pm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Request.PmTareaTecnico
{
    public class PmTareaTecnicoRequest
    {
        public int Id { get; set; }
        public int IdPmDetalle { get; set; }

        public List<int> IdTecnicos { get; set; } = new List<int>();

        public DateTime FechaInicialActividad { get; set; }

        public DateTime? FechaFinalActividad { get; set; }

        public decimal? DuracionActividad { get; set; }

        public bool? Activo { get; set; }

        public string? Descripcion { get; set; }

        public int IdTipoActividad { get; set; }

        public int IdTipoDemora { get; set; } //Campo para la transaccion en PMTareaDemora

        //Campos del PmDetalle
        public PmDetallesRequest? DatosTarea { get; set; }

        
    }
}
