using DLTD.GestionPm.Comun;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Request.CheckList
{
    public class CheckListRequest
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = Constantes.ErrorSelect)]
        public int IdTipoPm { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = Constantes.ErrorSelect)]
        public int IdModelo { get; set; }

        [Required(ErrorMessage = Constantes.ErrorMessage)]
        public string Descripcion { get; set; } = null!;

        public string Status { get; set; } = "Activo";

        //Detalles
        public List<CheckListDetallesRequest> Detalles { get; set; } = new();
    }
}
