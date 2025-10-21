using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class Ruta : EntidadBase
{
    

    public string Descripcion { get; set; } = null!;

   

    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
