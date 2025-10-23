using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class TipoDemora : EntidadBase
{
    

    public string Descripcion { get; set; } = null!;

   

    public virtual ICollection<PmtareaDemora> PmtareaDemoras { get; set; } = new List<PmtareaDemora>();
}
