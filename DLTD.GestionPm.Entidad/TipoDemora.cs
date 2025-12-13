using DLTD.GestionPm.Entidad;
using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class TipoDemora: EntidadBase
{
    

    public string Descripcion { get; set; } = null!;

    
    public string Nombre { get; set; } = null!;

    public bool? AplicaDetencion { get; set; }

    public virtual ICollection<PmtareaDemora> PmtareaDemoras { get; set; } = new List<PmtareaDemora>();
}
