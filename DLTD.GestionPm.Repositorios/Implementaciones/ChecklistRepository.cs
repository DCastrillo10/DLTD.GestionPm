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
                               .ThenInclude(d => d.IdRutaNavigation)
                               .FirstAsync(p => p.Id == id);
            return master;
        }
        public async Task AddMasterDetailsAsync(PmcheckList master)
        {
            await _contexto.PmcheckLists.AddAsync(master);
            await _contexto.SaveChangesAsync();
        }

        public async Task<bool> FindCheckList(int idTipopm, int idModelo)
        {
            var checkList = await _contexto.PmcheckLists
                            .AnyAsync(p => p.IdTipoPm == idTipopm && p.IdModelo == idModelo && p.Status == "Activo");
            return checkList;
        }
    }
}
