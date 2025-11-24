using DLTD.GestionPm.Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Request.Maquina
{
    public class MaquinaRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string Codigo { get; set; } = null!;

        public string? Descripcion { get; set; }

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public int IdModelo { get; set; }

        public string Status { get; set; } = "Activo";
    }
}
