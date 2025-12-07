using DLTD.GestionPm.Entidad;
using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class TipoPm: EntidadBase
{
    

    public string Descripcion { get; set; } = null!;   

    public string Nombre { get; set; } = null!;
    public virtual ICollection<PmcheckList> PmcheckLists { get; set; } = new List<PmcheckList>();
    public virtual ICollection<Pm> Pms { get; set; } = new List<Pm>();
}
