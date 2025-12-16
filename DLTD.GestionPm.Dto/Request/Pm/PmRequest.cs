using DLTD.GestionPm.Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Request.Pm
{
    public class PmRequest
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = Constantes.ErrorSelect)]
        public int IdTipoPm { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = Constantes.ErrorSelect)]
        public int IdModelo { get; set; }

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string WorkOrder { get; set; } = null!;

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string NoEquipo { get; set; } = null!;

        public string? NoHangar { get; set; }

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public decimal? HorometroActual { get; set; }

        
        public decimal? HorometroPrevio { get; set; } = 0;

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public DateTime? FechaInicialPm { get; set; }

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public DateTime? FechaFinalPm { get; set; }

        [Range(0.01, 999999.00, ErrorMessage ="La duracion debe ser mayor a cero.")]
        public decimal? Duracion { get; set; }

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string? StatusPm { get; set; } = "Pendiente";

        public string? Observacion { get; set; }

        public string Status { get; set; } = "Activo";

        //Detalles
        public List<PmDetallesRequest> PmDetalles { get; set; } = new();


        //Metodo para obligar al usuario a completar las fechas y horas del PM
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    // 1. Validar que la duración sea > 0 (Tu validación original)
        //    if (Duracion.HasValue && Duracion.Value <= 0)
        //    {
        //        yield return new ValidationResult("La duración calculada debe ser mayor a cero.", new[] { nameof(Duracion) });
        //    }

        //    // 2. Validar que la hora no sea 00:00:00 (hora por defecto si solo se selecciona la fecha)
        //    if (FechaInicialPm.HasValue && FechaInicialPm.Value.TimeOfDay == TimeSpan.Zero)
        //    {
        //        yield return new ValidationResult("Debe completar la hora en el campo Fecha y Hora Inicial.", new[] { nameof(FechaInicialPm) });
        //    }

        //    if (FechaFinalPm.HasValue && FechaFinalPm.Value.TimeOfDay == TimeSpan.Zero)
        //    {
        //        yield return new ValidationResult("Debe completar la hora en el campo Fecha y Hora Inicial.", new[] { nameof(FechaInicialPm) });
        //    }
        //}
    }
}
