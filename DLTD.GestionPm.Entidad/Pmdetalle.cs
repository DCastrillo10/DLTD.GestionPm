﻿using System;
using System.Collections.Generic;

namespace DLTD.GestionPm.Entidad;

public partial class Pmdetalle : EntidadBase
{
    

    public int IdPm { get; set; }

    public int IdRuta { get; set; }

    public int IdTarea { get; set; }

    public DateTime? FechaInicialTarea { get; set; }

    public DateTime? FechaFinalTarea { get; set; }

    /// <summary>
    /// Duracion de la tarea en la ejecucion
    /// </summary>
    public decimal? DuracionTarea { get; set; }

    /// <summary>
    /// Programado,Enprogreso,Completado
    /// </summary>
    public string? StatusTarea { get; set; }

    public bool? Realizado { get; set; }

    public decimal? Valor1 { get; set; }

    public decimal? Valor2 { get; set; }

    public decimal? Valor3 { get; set; }

    public string? Observacion { get; set; }

    /// <summary>
    /// Duracion de la tarea desde el parametro
    /// </summary>
    public decimal? Duracion { get; set; }

    /// <summary>
    /// Numero Tecnicos desde la parametrizacion
    /// </summary>
    public int? NoTecnicos { get; set; }

    public DateTime? FechaActualizacion { get; set; }

   

    public virtual Pm IdPmNavigation { get; set; } = null!;

    public virtual Tarea IdTareaNavigation { get; set; } = null!;

    public virtual ICollection<PmtareaActividad> PmtareaActividads { get; set; } = new List<PmtareaActividad>();

    public virtual ICollection<PmtareaDemora> PmtareaDemoras { get; set; } = new List<PmtareaDemora>();

    public virtual ICollection<PmtareaHallazgo> PmtareaHallazgos { get; set; } = new List<PmtareaHallazgo>();

    public virtual ICollection<PmtareaTecnico> PmtareaTecnicos { get; set; } = new List<PmtareaTecnico>();
}
