using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Request.Email
{
    public class EmailSettings
    {
        public string Server { get; set; } = default!;
        public int Port { get; set; } 
        public string User { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
