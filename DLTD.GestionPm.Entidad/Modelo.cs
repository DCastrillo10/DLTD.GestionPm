using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class Modelo : EntidadBase
{
    

    public string Modelo1 { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int IdMarca { get; set; }

    

    public virtual Marca IdMarcaNavigation { get; set; } = null!;

    public virtual ICollection<Maquina> Maquinas { get; set; } = new List<Maquina>();

    public virtual ICollection<Pm> Pms { get; set; } = new List<Pm>();
}
