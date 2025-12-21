using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class Tecnico : EntidadBase
{
    

    
    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? TurnoActual { get; set; }

    public string NoIdentificacion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public string? Especialidad { get; set; }


    

    public virtual ICollection<PmTareaDemora> PmTareaDemoras { get; set; } = new List<PmTareaDemora>();

    public virtual ICollection<PmTareaHallazgo> PmTareaHallazgos { get; set; } = new List<PmTareaHallazgo>();

    public virtual ICollection<PmTareaTecnico> PmTareaTecnicos { get; set; } = new List<PmTareaTecnico>();
    
    public virtual ICollection<GrupoTrabajo> GrupoTrabajoIdTecnicoPrincipalNavigations { get; set; } = new List<GrupoTrabajo>();
    public virtual ICollection<GrupoTrabajo> GrupoTrabajoIdTecnicoVinculadoNavigations { get; set; } = new List<GrupoTrabajo>();
}
