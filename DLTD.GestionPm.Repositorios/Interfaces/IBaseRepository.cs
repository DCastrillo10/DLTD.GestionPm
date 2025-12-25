using DLTD.GestionPm.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLTD.GestionPm.Repositorios.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : EntidadBase
    {
        Task<ICollection<TEntity>> ListAsync();
        Task<ICollection<TResult>> ListAsync<TResult>
        (
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selector
        );
        Task<(ICollection<TResult> Result, int TotalElements)> ListAsync<TResult, TKey>
        (
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, TKey>> orderBy,
            int page = 1, int rows = 10
        );

        Task<TEntity?> FindAsync(int id);
        Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);
        
        Task<TEntity> AddAsync(TEntity request);
        Task AddRangeAsync(IEnumerable<TEntity> request);        
        Task DeleteAsync(int id);
    }
}
