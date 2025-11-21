using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Response
{
    public class PaginationResponse<T>: BaseResponse
    {
        public ICollection<T>? Result { get; set; }
        public int TotalRows { get; set; }
        public int TotalPages { get; set; }
    }
}
