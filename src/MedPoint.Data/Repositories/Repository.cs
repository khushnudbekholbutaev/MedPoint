using Microsoft.EntityFrameworkCore;
using MedPoint.Data.DbContexts;
using MedPoint.Data.IRepositories;
using MedPoint.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPoint.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
    {
        private readonly AppDbContext dbContext;
        private readonly DbSet<TEntity> dbSet;
      
        public Repository (AppDbContext dbContext)
            => (this.dbContext, this.dbSet) = (dbContext, dbContext.Set<TEntity>());
        
        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await dbSet.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            var result = await dbSet
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync(cancellationToken);

            return result > 0;
        }

        public async Task<TEntity> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var result = await dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            return result;
        }

        public IQueryable<TEntity> SelectAll()
            => this.dbSet;

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var result = dbSet.Update(entity).Entity;
            await dbContext.SaveChangesAsync();
            return result;
        }
    }
}
