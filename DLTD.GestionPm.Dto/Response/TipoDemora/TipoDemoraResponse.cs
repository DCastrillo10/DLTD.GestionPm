using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Response.TipoDemora
{
    public class TipoDemoraResponse
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = default!;
        public string Nombre { get; set; } = default!;
        public string Status { get; set; } = default!;
    }
}
