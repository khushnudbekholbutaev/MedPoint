using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Data.IRepositories
{
    public interface IRepository<TEntity> 
    {
        public Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
        public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        public Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
        public Task<TEntity> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        public IQueryable<TEntity> SelectAll();
    }
}
