using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Response.Tecnico
{
    public class ListaTecnicoResponse
    {
        public int Id { get; set; }

        public string Nombres { get; set; } = null!;

        public string Apellidos { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? TurnoActual { get; set; }

        public string NoIdentificacion { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public string Codigo { get; set; } = null!;

        public string? Especialidad { get; set; }
        public string Status { get; set; } = default!;

        public string NombresCompletos => $"{Nombres} {Apellidos}";
    }
}
