using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Response.Maquina
{
    public class ListaMaquinaResponse
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;

        public string? Descripcion { get; set; }

        public string Modelo { get; set; } = null!;
        public string Status { get; set; } = default!;
    }
}
