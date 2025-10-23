using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Response
{
    public class BaseResponse
    {
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class BaseResponse<T>: BaseResponse
    {
        public T? Result { get; set; }
    }
}
