using DLTD.GestionPm.Entidad;
using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class TipoActividad: EntidadBase
{
    

    public string Descripcion { get; set; } = null!;    

    public string Nombre { get; set; } = null!;

    public virtual ICollection<PmtareaActividad> PmtareaActividads { get; set; } = new List<PmtareaActividad>();
}
