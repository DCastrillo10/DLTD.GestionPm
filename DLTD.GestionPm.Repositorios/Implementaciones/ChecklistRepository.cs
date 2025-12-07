using DLTD.GestionPm.AccesoDatos.Contexto;
using DLTD.GestionPm.Entidad;
using DLTD.GestionPm.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Repositorios.Implementaciones
{
    public class CheckListRepository : BaseRepository<PmcheckList>, ICheckListRepository
    {        
        public CheckListRepository(GestionPmBdContext contexto): base(contexto) 
        {
                
        }
        public async Task<PmcheckList?> FindByIdWithDetailAsync(int id)
        {
            var master = await _contexto.PmcheckLists
                               .Include(p => p.PmcheckListDetalles)
                               .FirstAsync(p => p.Id == id);
            return master;
        }
        public async Task AddMasterDetailsAsync(PmcheckList master)
        {
            await _contexto.PmcheckLists.AddAsync(master);
            await _contexto.SaveChangesAsync();
        }

        
    }
}
