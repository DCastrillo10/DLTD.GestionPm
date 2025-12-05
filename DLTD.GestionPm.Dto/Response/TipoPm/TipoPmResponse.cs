using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Response.TipoPm
{
    public class TipoPmResponse
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = default!;
        public string Nombre { get; set; } = default!;
        public string Status { get; set; } = default!;
    }
}
