using DLTD.GestionPm.Dto.Request.CheckList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Response.CheckList
{
    public class CheckListResponse
    {
        public int Id { get; set; }
        public int IdTipoPm { get; set; }

        public int IdModelo { get; set; }
        public string Descripcion { get; set; } = null!;
        

        public string Status { get; set; } = default!;

        //Detalles
        public List<CheckListDetallesResponse> Detalles { get; set; } = new();
    }
}
