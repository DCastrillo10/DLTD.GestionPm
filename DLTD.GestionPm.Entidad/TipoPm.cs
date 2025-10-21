﻿using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class TipoPm : EntidadBase
{
    

    public string Descripcion { get; set; } = null!;

   

    public virtual ICollection<Pm> Pms { get; set; } = new List<Pm>();
}
