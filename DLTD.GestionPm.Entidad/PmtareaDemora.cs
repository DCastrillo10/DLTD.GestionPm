using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class PmTareaDemora : EntidadBase
{
    

    public int IdPmDetalle { get; set; }

    public int IdTipoDemora { get; set; }

    public DateTime FechaInicialDemora { get; set; }

    public DateTime? FechaFinalDemora { get; set; }

    public decimal? DuracionDemora { get; set; }

    public string? Descripcion { get; set; }

   

    public int IdTecnico { get; set; }
    public bool? Activo { get; set; }

    public virtual Pmdetalle IdPmDetalleNavigation { get; set; } = null!;

    public virtual Tecnico IdTecnicoNavigation { get; set; } = null!;

    public virtual TipoDemora IdTipoDemoraNavigation { get; set; } = null!;
}
