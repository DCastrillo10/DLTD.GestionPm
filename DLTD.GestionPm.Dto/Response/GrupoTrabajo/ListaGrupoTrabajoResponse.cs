using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Response.GrupoTrabajo
{
    public class ListaGrupoTrabajoResponse
    {
        public int Id { get; set; }
        public int IdTecnicoPrincipal { get; set; }
        public int IdTecnicoVinculado { get; set; }       

        public DateTime? FechaVinculacion { get; set; }

        public string Nombres { get; set; } = null!;

        public string Apellidos { get; set; } = null!;        

        public string NoIdentificacion { get; set; } = null!;        

        public string Codigo { get; set; } = null!;

        public string? Especialidad { get; set; }        
        
        public string NombresCompletos => $"{(Nombres ?? "").Trim()} {(Apellidos ?? "").Trim()}".Trim();
    }
}
