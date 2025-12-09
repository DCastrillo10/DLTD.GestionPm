using DLTD.GestionPm.Entidad;
using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class Ruta: EntidadBase
{
    
    public string Descripcion { get; set; } = null!;   

    public string Nombre { get; set; } = null!;
    public virtual ICollection<PmcheckListDetalle> PmcheckListDetalles { get; set; } = new List<PmcheckListDetalle>();
    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
