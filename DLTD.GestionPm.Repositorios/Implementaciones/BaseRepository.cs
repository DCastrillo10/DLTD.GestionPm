using DLTD.GestionPm.AccesoDatos.Contexto;
using DLTD.GestionPm.Entidad;
using DLTD.GestionPm.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Repositorios.Implementaciones
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : EntidadBase
    {
        protected readonly GestionPmBdContext _contexto;

        public BaseRepository(GestionPmBdContext contexto)
        {
            _contexto = contexto;
        }

        //CRUD BASE

        #region Operaciones de Lectura
        public async Task<ICollection<TEntity>> ListAsync()
        {
            return await _contexto.Set<TEntity>()
                .Where(e => e.Status == "Activo")
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<TResult>> ListAsync<TResult>
        (
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selector
        )
        {
            return await _contexto.Set<TEntity>()
                        .Where(predicate)
                        .Select(selector)
                        .ToListAsync();
        }

        public async Task<(ICollection<TResult> Result, int TotalElements)> ListAsync<TResult, TKey>
        (
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, TKey>> orderBy,
            int page = 1, int rows = 10
        )
        {
            var result = await _contexto.Set<TEntity>()
                        .Where(predicate)
                        .Skip((page - 1) * rows)
                        .Take(rows)
                        .OrderBy(orderBy)
                        .Select(selector)
                        .ToListAsync();

            var totalElements = await _contexto.Set<TEntity>()
                                        .Where(predicate)
                                        .CountAsync();

            return (result, totalElements);
        }


        public async Task<TEntity?> FindAsync(int id)
        {
            return await _contexto.Set<TEntity>().FirstOrDefaultAsync(p => p.Status == "Activo" && p.Id == id);
        }
        #endregion

        #region Operaciones de Escritura

        public async Task<TEntity> AddAsync(TEntity request)
        {
            var result = await _contexto.Set<TEntity>().AddAsync(request);
            await _contexto.SaveChangesAsync();
            return result.Entity;
        }

        #endregion

        #region Operaciones de Actualizacion

        public async Task UpdateAsync()
        {
            await _contexto.SaveChangesAsync();
        }

        #endregion

        #region Operaciones de Eliminacion

        public async Task DeleteAsync(int id)
        {
            await _contexto.Set<TEntity>()
                .Where(p => p.Id == id)
                .ExecuteUpdateAsync(p => p.SetProperty(x => x.Status, "Inactivo"));
        }

        #endregion
    }
}
