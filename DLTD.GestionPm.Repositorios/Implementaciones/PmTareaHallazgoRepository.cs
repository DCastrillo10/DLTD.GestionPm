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
    public class PmTareaHallazgoRepository(GestionPmBdContext contexto): BaseRepository<PmTareaHallazgo>(contexto), IPmTareaHallazgoRepository
    {
        public async Task<string?> FindEquipo(int id)
        {
            var Equipo = await _contexto.PmDetalles
                               .Where(p => p.Id == id)
                               .Select(p => p.IdPmNavigation.NoEquipo)
                               .FirstOrDefaultAsync();
            return Equipo;
        }
    }
}
