using DLTD.GestionPm.Entidad;
using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class PmcheckList: EntidadBase
{
    

    public int IdTipoPm { get; set; }

    public int IdModelo { get; set; }

    public string Descripcion { get; set; } = null!;
    public virtual Modelo IdModeloNavigation { get; set; } = null!;

    public virtual TipoPm IdTipoPmNavigation { get; set; } = null!;

    public virtual ICollection<PmcheckListDetalle> PmcheckListDetalles { get; set; } = new List<PmcheckListDetalle>();
}
