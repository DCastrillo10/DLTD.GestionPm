using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Response.Tarea
{
    public class ListaTareaResponse
    {
        public int Id { get; set; }

        public string Descripcion { get; set; } = null!;

        public string Ruta { get; set; } = default!;

        public string CodigoTarea { get; set; } = null!;

        public decimal Duracion { get; set; }

        public int NoTecnicos { get; set; }

        public string Status { get; set; } = default!;
    }
}
