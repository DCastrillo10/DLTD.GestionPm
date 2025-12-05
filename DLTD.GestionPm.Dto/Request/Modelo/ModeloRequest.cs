using DLTD.GestionPm.Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Request.Modelo
{
    public class ModeloRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string Referencia { get; set; } = null!;

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string? Descripcion { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = Constantes.ErrorSelect)]
        public int IdMarca { get; set; }
        public string Status { get; set; } = "Activo";
    }
}
