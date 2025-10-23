﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Dto.Request.Security
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public int ExpirationMinutes { get; set; }
    }
}
