using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class GrupoTrabajo: EntidadBase
{
    

    public int IdTecnicoPrincipal { get; set; }

    public int IdTecnicoVinculado { get; set; }

    public DateTime? FechaVinculacion { get; set; }

    
    public virtual Tecnico IdTecnicoPrincipalNavigation { get; set; } = null!;

    public virtual Tecnico IdTecnicoVinculadoNavigation { get; set; } = null!;
}
