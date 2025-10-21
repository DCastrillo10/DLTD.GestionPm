using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class TipoHallazgo : EntidadBase
{
    

    public string Descripcion { get; set; } = null!;

   

    public virtual ICollection<PmtareaHallazgo> PmtareaHallazgos { get; set; } = new List<PmtareaHallazgo>();
}
