using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class TipoActividad: EntidadBase
{
    

    /// <summary>
    /// Iniciar Turno,
    /// Reanudar Turno,
    /// Finalizar Turno
    /// </summary>
    public string Descripcion { get; set; } = null!;

    
    public string Nombre { get; set; } = null!;

    public virtual ICollection<PmTareaTecnico> PmTareaTecnicos { get; set; } = new List<PmTareaTecnico>();
}
