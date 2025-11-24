using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Request
{
    public class PaginationRequest
    {
        public string? Filter { get; set; }
        public int Page { get; set; } = 1;
        public int Rows { get; set; } = 2;
    }
}
