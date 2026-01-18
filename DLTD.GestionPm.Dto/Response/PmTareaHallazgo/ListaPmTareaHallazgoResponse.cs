using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Response.PmTareaHallazgo
{
    public class ListaPmTareaHallazgoResponse
    {
        public int Id { get; set; }
        public int IdPmDetalle { get; set; }

        public string? TipoHallazgo { get; set; }

        public string? ImagenUrl { get; set; }

        public string Descripcion { get; set; } = null!;

        public DateTime FechaHallazgo { get; set; }

        public string ValidadoPor { get; set; } = default!;
        public string NoEquipo { get; set; } = null!;
        public string? Tecnicos { get; set; }

        public string Status { get; set; } = default!;

        public string UsuarioRegistro { get; set; } = default!;

        public DateTime FechaRegistro { get; set; }
    }
}
