using DLTD.GestionPm.Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Response.Tecnico
{
    public class TecnicoResponse
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

        //Son necesarios para la actualizacion del usuario en UserIdentity
        public int? IdUsuario { get; set; }
       
        public string Clave { get; set; } = null!;
       
        public string ConfirmarClave { get; set; } = null!;
    }
}
