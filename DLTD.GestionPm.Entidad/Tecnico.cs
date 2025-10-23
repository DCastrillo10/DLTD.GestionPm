using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class Tecnico : EntidadBase
{
    

    public string IdUser { get; set; } = null!;

    public string? Nombres { get; set; }

    public string? Apellidos { get; set; }

    public string? Especialidad { get; set; }

    public string? TurnoActual { get; set; }

    

    public virtual ICollection<PmtareaActividad> PmtareaActividads { get; set; } = new List<PmtareaActividad>();

    public virtual ICollection<PmtareaDemora> PmtareaDemoras { get; set; } = new List<PmtareaDemora>();

    public virtual ICollection<PmtareaHallazgo> PmtareaHallazgos { get; set; } = new List<PmtareaHallazgo>();

    public virtual ICollection<PmtareaTecnico> PmtareaTecnicos { get; set; } = new List<PmtareaTecnico>();

    public virtual ICollection<PmtecnicoTurno> PmtecnicoTurnos { get; set; } = new List<PmtecnicoTurno>();
}
