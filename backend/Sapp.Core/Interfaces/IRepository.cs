using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Sapp.Core.Entities;

namespace Sapp.Core.Interfaces
{
    public interface IRepository<TEntity, TKey>
        where TKey : IComparable
        where TEntity : EntityBase
    {
        public Task<TEntity> GetByIdAsync(TKey id, CancellationToken token = default);

        public Task<IEnumerable<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>> filter,
            CancellationToken token = default);

        public Task<TEntity> CreateAsync(TEntity entity, CancellationToken token = default);
        public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token = default);
        public Task<TEntity> DeleteAsync(TEntity entity, CancellationToken token = default);
    }
}