using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class Pm : EntidadBase
{
    

    public int IdTipoPm { get; set; }

    public int IdModelo { get; set; }

    public string WorkOrder { get; set; } = null!;

    public string NoEquipo { get; set; } = null!;

    public string? NoHangar { get; set; }

    public decimal? Horometro { get; set; }

    public DateTime? FechaInicialPm { get; set; }

    public DateTime? FechaFinalPm { get; set; }

    public decimal? Duracion { get; set; }

    public string? StatusPm { get; set; }

    public string? Observacion { get; set; }

    

    public virtual Modelo IdModeloNavigation { get; set; } = null!;

    public virtual TipoPm IdTipoPmNavigation { get; set; } = null!;

    public virtual ICollection<Pmdetalle> Pmdetalles { get; set; } = new List<Pmdetalle>();

    public virtual ICollection<PmtecnicoTurno> PmtecnicoTurnos { get; set; } = new List<PmtecnicoTurno>();
}
