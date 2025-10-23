using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class Horometro: EntidadBase
{
    

    public int IdMaquina { get; set; }

    public decimal? HorometroPrevio { get; set; }

    public decimal? HorometroActual { get; set; }

    public string? Observacion { get; set; }

    

    public virtual Maquina IdMaquinaNavigation { get; set; } = null!;
}
