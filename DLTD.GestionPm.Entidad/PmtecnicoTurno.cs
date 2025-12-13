using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class PmtecnicoTurno : EntidadBase
{
    

    public int IdPm { get; set; }

    public int IdTecnico { get; set; }

    /// <summary>
    /// Dia o Noche
    /// </summary>
    public string Turno { get; set; } = null!;

    public DateTime? FechaInicialTurno { get; set; }

    public DateTime? FechaFinalTurno { get; set; }

   

    public decimal? DuracionTurno { get; set; }
    public string? Descripcion { get; set; }

    public virtual Pm IdPmNavigation { get; set; } = null!;

    public virtual Tecnico IdTecnicoNavigation { get; set; } = null!;
}
