using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class PmtareaTecnico : EntidadBase
{
    

    public int IdPmDetalle { get; set; }

    public string IdTecnico { get; set; } = null!;

    public DateTime FechaInicialAsignacion { get; set; }

    public DateTime? FechaFinalAsignacion { get; set; }

    public decimal? DuracionAsignacion { get; set; }

    public bool? Activo { get; set; }

    public virtual Pmdetalle IdPmDetalleNavigation { get; set; } = null!;
}
