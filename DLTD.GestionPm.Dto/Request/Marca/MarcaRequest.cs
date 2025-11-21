using DLTD.GestionPm.Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Request.Marca
{
    public class MarcaRequest
    {
        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string Descripcion { get; set; } = default!;
    }
}
