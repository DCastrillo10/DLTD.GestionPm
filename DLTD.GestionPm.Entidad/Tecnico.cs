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


    public virtual ICollection<PmtareaActividad> PmtareaActividads { get; set; } = new List<PmtareaActividad>();

    public virtual ICollection<PmtareaDemora> PmtareaDemoras { get; set; } = new List<PmtareaDemora>();

    public virtual ICollection<PmtareaHallazgo> PmtareaHallazgos { get; set; } = new List<PmtareaHallazgo>();

    public virtual ICollection<PmtareaTecnico> PmtareaTecnicos { get; set; } = new List<PmtareaTecnico>();

    public virtual ICollection<PmtecnicoTurno> PmtecnicoTurnos { get; set; } = new List<PmtecnicoTurno>();
}
