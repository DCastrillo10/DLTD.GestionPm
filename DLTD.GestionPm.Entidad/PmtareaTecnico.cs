using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class PmtareaTecnico: EntidadBase
{
    

    public int IdPmDetalle { get; set; }

    public int IdTecnico { get; set; }

    public DateTime FechaInicialAsignacion { get; set; }

    public DateTime? FechaFinalAsignacion { get; set; }

    public decimal? DuracionAsignacion { get; set; }

    public bool? Activo { get; set; }

    

    public string? Descripcion { get; set; }

    public int IdTipoActividad { get; set; }

    public virtual Pmdetalle IdPmDetalleNavigation { get; set; } = null!;

    public virtual Tecnico IdTecnicoNavigation { get; set; } = null!;

    public virtual TipoActividad IdTipoActividadNavigation { get; set; } = null!;
}
