using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Response.CheckList
{
    public class ListaCheckListResponse
    {
        public int Id { get; set; }
        public string TipoPm { get; set; } = default!;

        public string Modelo { get; set; } = default!;

        public string Descripcion { get; set; } = null!;

        public string Status { get; set; } = default!;
    }
}
