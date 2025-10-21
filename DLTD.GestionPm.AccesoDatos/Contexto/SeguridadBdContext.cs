using DLTD.GestionPm.AccesoDatos.Configuracion;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.AccesoDatos.Contexto
{
    public class SeguridadBdContext: IdentityDbContext<SecurityEntity>
    {
        public SeguridadBdContext()
        {
            
        }

        public SeguridadBdContext(DbContextOptions<SeguridadBdContext> options): base(options) 
        {
            
        }

    }
}
