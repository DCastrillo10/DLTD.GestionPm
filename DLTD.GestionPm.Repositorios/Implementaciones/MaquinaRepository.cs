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
    public class MaquinaRepository(GestionPmBdContext contexto) : BaseRepository<Maquina>(contexto), IMaquinaRepository
    {
        public async Task<Maquina?> FindMaquina(string NoEquipo)
        {
            var Equipo = await _contexto.Maquinas
                         .FirstOrDefaultAsync(p => p.Codigo == NoEquipo && p.Status == "Activo");
            return Equipo;
        }
    }
}
