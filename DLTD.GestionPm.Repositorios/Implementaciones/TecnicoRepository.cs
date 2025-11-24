using DLTD.GestionPm.AccesoDatos.Configuracion;
using DLTD.GestionPm.AccesoDatos.Contexto;
using DLTD.GestionPm.Comun;
using DLTD.GestionPm.Entidad;
using DLTD.GestionPm.Repositorios.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Repositorios.Implementaciones
{
    public class TecnicoRepository: BaseRepository<Tecnico>, ITecnicoRepository
    {
        private readonly UserManager<SecurityEntity> _userManager;

        public TecnicoRepository(GestionPmBdContext contexto, UserManager<SecurityEntity> userManager) : base(contexto)
        {
            _userManager = userManager;
        }
        
        public async Task<(Tecnico? tecnico, IdentityResult identityresult)> CreateAsync(Tecnico request, string usuario, string clave)
        {
            await using (var transaction = await _contexto.Database.BeginTransactionAsync())
            {
                try
                {
                    var tecnico = await AddAsync(request);

                        var user = new SecurityEntity()
                        {
                            IdUsuario = tecnico.Id,
                            Codigo = tecnico.Codigo,
                            NombresCompletos = $"{tecnico.Nombres} {tecnico.Apellidos}",
                            UserName = usuario,
                            Email = tecnico.Email
                        };

                        var result = await _userManager.CreateAsync(user, clave);
                        if (!result.Succeeded)
                        {
                            await transaction.RollbackAsync();
                            return (null,result);
                        }

                        var roleResult = await _userManager.AddToRoleAsync(user, CatalogRoles.Tecnico);
                        if (!roleResult.Succeeded)
                        {
                            await transaction.RollbackAsync();
                            return (null,roleResult);
                        }

                        await transaction.CommitAsync();
                        return (tecnico,IdentityResult.Success);                        
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return (null,IdentityResult.Failed(new IdentityError { Description = ex.Message }));
                }
                
            }
        }       

    }
}
