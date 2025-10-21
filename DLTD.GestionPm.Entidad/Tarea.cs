using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class Tarea : EntidadBase
{
    

    public string Descripcion { get; set; } = null!;

    public int IdRuta { get; set; }

   

    public string CodigoTarea { get; set; } = null!;

    public decimal Duracion { get; set; }

    public int NoTecnicos { get; set; }

    public virtual Ruta IdRutaNavigation { get; set; } = null!;

    public virtual ICollection<Pmdetalle> Pmdetalles { get; set; } = new List<Pmdetalle>();
}
