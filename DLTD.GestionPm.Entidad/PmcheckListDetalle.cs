using DLTD.GestionPm.Entidad;
using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class PmcheckListDetalle: EntidadBase
{
    
    public int IdPmCheckList { get; set; }

    public int IdRuta { get; set; }

    
    public virtual PmcheckList IdPmCheckListNavigation { get; set; } = null!;
    public virtual Ruta IdRutaNavigation { get; set; } = null!;
}
