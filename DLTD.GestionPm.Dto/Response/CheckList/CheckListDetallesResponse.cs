using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Response.CheckList
{
    public class CheckListDetallesResponse
    {
        public int Id { get; set; }
        public int IdPmCheckList { get; set; }

        public int IdRuta { get; set; }
        public string Nombre { get; set; } = default!;
        public string Status { get; set; } = default!;
    }
}
