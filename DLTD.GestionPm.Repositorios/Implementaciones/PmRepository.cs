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
    public class PmRepository : BaseRepository<Pm>, IPmRepository
    {        
        public PmRepository(GestionPmBdContext contexto): base(contexto) 
        {
                
        }
        public async Task<Pm?> FindByIdWithDetailAsync(int id)
        {
            var master = await _contexto.Pms
                               .Include(p => p.PmDetalles)
                                    .ThenInclude(d => d.IdTareaNavigation)
                                        .ThenInclude(t => t.IdRutaNavigation)
                               .FirstAsync(p => p.Id == id);
            return master;
        }
        public async Task AddMasterDetailsAsync(Pm master)
        {
            await _contexto.Pms.AddAsync(master);
            await _contexto.SaveChangesAsync();
        }

        public async Task<bool> FindPm(int idTipopm, int idModelo, string NoEquipo,string WorkOrder)
        {
            var Pm = await _contexto.Pms
                            .AnyAsync(p => p.IdTipoPm == idTipopm && p.IdModelo == idModelo && p.NoEquipo == NoEquipo && p.WorkOrder == WorkOrder && p.Status != "Eliminado");
            return Pm;
        }

        public async Task<IEnumerable<Tarea>> FindTareas(int idTipoPm, int idModelo)
        {
            var idRutas = await _contexto.PmcheckLists
                                .Where(p => p.IdTipoPm == idTipoPm && p.IdModelo == idModelo)
                                .SelectMany(p => p.PmcheckListDetalles)                                
                                .Select(d => d.IdRuta)
                                .Distinct()
                                .ToListAsync();

            if (!idRutas.Any())
            {
                return Enumerable.Empty<Tarea>();
            }

            var tareas = await _contexto.Tareas
                               .Include(t => t.IdRutaNavigation)
                               .Where(t => idRutas.Contains(t.IdRuta))
                               .ToListAsync();
            return tareas;
           
        }


        //Estos metodos son para la entidad detalle como tal, lo hago aqui para evitar crear mas archivos de repositorios. Si crece el projecto, entonces lo hago.
        public async Task<Pmdetalle?> GetDetalleTareaPmById(int id)
        {
            var detalleTareaPm = await _contexto.PmDetalles
                                        .Include(t => t.IdTareaNavigation)
                                            .ThenInclude(t => t.IdRutaNavigation)
                                        .FirstOrDefaultAsync(p => p.Id == id);
            return detalleTareaPm;
        }

        public async Task<Pmdetalle> UpdateDetalle(Pmdetalle pmdetalle)
        {
            _contexto.PmDetalles.Update(pmdetalle);
            await _contexto.SaveChangesAsync();
            return pmdetalle;
        }
    }
}
