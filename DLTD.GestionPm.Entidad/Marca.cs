using DLTD.GestionPm.Entidad;
using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class Marca:EntidadBase
{
    
    public string Descripcion { get; set; } = null!;   

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Modelo> Modelos { get; set; } = new List<Modelo>();
}
