using DLTD.GestionPm.Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Request.Login
{
    public class LoginRequest
    {
        [Required(ErrorMessage = Constantes.ErrorMessage)]
        [Display(Name ="Usuario")]
        public string UserName { get; set; } = default!;

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = default!;
    }
}
