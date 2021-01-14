using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Base
{
    public interface IRepositoryBase<TEntity>
        where TEntity:EntityBase
    {
        TEntity Find(Guid id);
        Task<TEntity> FindAsync(Guid id);

        TEntity FindBy(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> filter);
        
        IQueryable<TEntity> List(Expression<Func<TEntity, bool>> filter);

        bool Any(Expression<Func<TEntity, bool>> where);

        TEntity Add(TEntity entity);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
        TEntity Update(TEntity entity);
        IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities);
        void Delete(TEntity id);
        void DeleteRange(IEnumerable<TEntity> entities);

        bool Commit();
        Task<bool> CommitAsync();
    }
}
