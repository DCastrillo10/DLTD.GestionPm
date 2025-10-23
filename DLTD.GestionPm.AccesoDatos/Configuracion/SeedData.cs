using DLTD.GestionPm.Comun;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.AccesoDatos.Configuracion
{
    public class SeedData
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<SecurityEntity>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] Roles = { CatalogRoles.Administrador, CatalogRoles.Tecnico, CatalogRoles.Supervisor };

            foreach (var role in Roles)
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var admin = new SecurityEntity()
            {
                UserName = "admin",
                Codigo = "Admin001",
                Email = "admin@gmail.com",
                NombresCompletos="Administrador",
                IdUsuario=0
            };

            await userManager.CreateAsync(admin, "Password2025");
            await userManager.AddToRoleAsync(admin, CatalogRoles.Administrador);
        }
    }
}
