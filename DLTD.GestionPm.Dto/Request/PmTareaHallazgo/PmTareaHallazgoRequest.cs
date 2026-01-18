using DLTD.GestionPm.Comun;
using DLTD.GestionPm.Dto.Request.Azure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Request.PmTareaHallazgo
{
    public class PmTareaHallazgoRequest
    {
        public int Id { get; set; }
        public int IdPmDetalle { get; set; }

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public int IdTipoHallazgo { get; set; }

        public AzureBlobRequest Soporte { get; set; } = new();

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string Descripcion { get; set; } = null!;

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public DateTime FechaHallazgo { get; set; }

        public string? ValidadoPor { get; set; }

        public string Status { get; set; } = "Activo";

        
        public string NoEquipo { get; set; } = null!;
        public string? Tecnicos { get; set; }


    }
}
