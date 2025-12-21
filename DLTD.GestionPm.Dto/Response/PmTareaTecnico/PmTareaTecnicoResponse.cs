using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Response.PmTareaTecnico
{
    public class PmTareaTecnicoResponse
    {
        public int Id { get; set; }
        public int IdPmDetalle { get; set; }
        public int IdTecnico { get; set; }
        public DateTime FechaInicialActividad { get; set; }
        public DateTime? FechaFinalActividad { get; set; }
        public decimal? DuracionActividad { get; set; }
        public bool? Activo { get; set; }
        public string? Descripcion { get; set; }
        public int IdTipoActividad { get; set; }
        public string Status { get; set; } = default!;

        public string? UsuarioRegistro { get; set; }

        public DateTime FechaRegistro { get; set; }

        //nuevas propiedades
        public string? TipoActividad { get; set; }
        public string? NombresTecnicos { get; set; }
    }
}
