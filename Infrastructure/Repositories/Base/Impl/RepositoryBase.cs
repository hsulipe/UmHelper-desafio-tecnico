using Domain.Models.Entities;
using Infrastructure.Databases.Postgres;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Base.Impl
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity:EntityBase
    {
        private readonly FinanceDbContext _context;

        protected RepositoryBase(FinanceDbContext context)
        {
            _context = context;
        }

        public TEntity Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            return entity;
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            var addMany = entities as TEntity[] ?? entities.ToArray();
            _context.Set<TEntity>().AddRange(addMany);
            return addMany;
        }

        public bool Commit()
            => _context.SaveChanges() > 0;

        public async Task<bool> CommitAsync()
            => await _context.SaveChangesAsync() > 0;

        public TEntity Find(Guid id)
        {
            var query = _context.Set<TEntity>();
            return query.Find(id);
        }

        public async Task<TEntity> FindAsync(Guid id)
        {
            var query = _context.Set<TEntity>();
            return await query.FindAsync(id);
        }

        public TEntity FindBy(Expression<Func<TEntity, bool>> filter)
        {
            var query = _context.Set<TEntity>();
            return query.FirstOrDefault(filter);
        }

        public async Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> filter)
        {
            var query = _context.Set<TEntity>();
            return await query.FirstOrDefaultAsync(filter);
        }

        public IQueryable<TEntity> List(Expression<Func<TEntity, bool>> filter)
            => _context.Set<TEntity>().Where(filter);

        public bool Any(Expression<Func<TEntity, bool>> @where)
            => _context.Set<TEntity>().Any(@where);

        public IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().UpdateRange(entities);
            return entities;
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
