using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sapp.Core.Entities;
using Sapp.Core.Interfaces;

namespace Sapp.Core.Persistence
{
    public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : EntityBase
        where TKey : IComparable
    {
        protected readonly DbContext DbContext;
        protected readonly DbSet<TEntity> DbSet;

        protected RepositoryBase(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TEntity>();
        }

        public Task<TEntity> GetByIdAsync(TKey id, CancellationToken token = default)
        {
            return DbContext
                .Set<TEntity>()
                .FindAsync(id)
                .AsTask();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>> filter,
            CancellationToken token = default)
        {
            return await DbSet
                .Where(filter)
                .ToListAsync(token);
        }

        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken token = default)
        {
            DbSet.Add(entity);
            await DbContext.SaveChangesAsync(token);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token = default)
        {
            DbSet.Update(entity);
            await DbContext.SaveChangesAsync(token);
            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken token = default)
        {
            DbSet.Remove(entity);
            await DbContext.SaveChangesAsync(token);
            return entity;
        }
    }
}