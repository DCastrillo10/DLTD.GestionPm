using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class TipoActividad : EntidadBase
{
    

    public string Descripcion { get; set; } = null!;

   

    public virtual ICollection<PmtareaActividad> PmtareaActividads { get; set; } = new List<PmtareaActividad>();
}
