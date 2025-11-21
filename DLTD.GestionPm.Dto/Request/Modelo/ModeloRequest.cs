using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Request.Modelo
{
    public class ModeloRequest
    {
        public int Id { get; set; }
        public string Referencia { get; set; } = null!;

        public string? Descripcion { get; set; }

        public int IdMarca { get; set; }
    }
}
