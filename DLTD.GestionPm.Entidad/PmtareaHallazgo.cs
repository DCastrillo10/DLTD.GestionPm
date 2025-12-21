using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class PmTareaHallazgo : EntidadBase
{
    

    public int IdPmDetalle { get; set; }

    public int IdTipoHallazgo { get; set; }

    public string? ImagenUrl { get; set; }

    public string Descripcion { get; set; } = null!;

    public DateTime FechaHallazgo { get; set; }

    public string? ValidadoPor { get; set; }

  

    public int IdTecnico { get; set; }

    public virtual Pmdetalle IdPmDetalleNavigation { get; set; } = null!;

    public virtual Tecnico IdTecnicoNavigation { get; set; } = null!;

    public virtual TipoHallazgo IdTipoHallazgoNavigation { get; set; } = null!;
}
