using DLTD.GestionPm.Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Request.CheckList
{
    public class CheckListDetallesRequest
    {
        public int Id { get; set; }        

        [Range(1, int.MaxValue, ErrorMessage = Constantes.ErrorSelect)]
        public int IdRuta { get; set; }

        public string Status { get; set; } = "Activo";
    }
}
