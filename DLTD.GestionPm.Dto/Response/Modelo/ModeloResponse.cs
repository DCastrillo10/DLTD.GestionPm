using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Response.Modelo
{
    public class ModeloResponse
    {
        public int Id { get; set; }
        public string Referencia { get; set; } = null!;

        public string? Descripcion { get; set; }

        public int IdMarca { get; set; }
        public string Status { get; set; } = default!;
    }
}
