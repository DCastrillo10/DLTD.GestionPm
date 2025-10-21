using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class PmtareaActividad : EntidadBase
{
    

    public int IdPmDetalle { get; set; }

    public string IdTecnico { get; set; } = null!;

    public DateTime FechaInicialActividad { get; set; }

    public DateTime? FechaFinalActividad { get; set; }

    public string? Descripcion { get; set; }

   

    public decimal? DuracionActividad { get; set; }

    public int IdTipoActividad { get; set; }

    public virtual Pmdetalle IdPmDetalleNavigation { get; set; } = null!;

    public virtual TipoActividad IdTipoActividadNavigation { get; set; } = null!;
}
