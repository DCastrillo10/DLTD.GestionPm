using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class Maquina : EntidadBase
{
    
    public string Codigo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int IdModelo { get; set; }

    public virtual ICollection<Horometro> Horometros { get; set; } = new List<Horometro>();

    public virtual Modelo IdModeloNavigation { get; set; } = null!;
}
