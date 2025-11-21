using DLTD.GestionPm.Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Request.Azure
{
    public class AzureBlobRequest
    {
        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string Base64 { get; set; } = default!;
        
        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string Name { get; set; } = default!;
    }
}
