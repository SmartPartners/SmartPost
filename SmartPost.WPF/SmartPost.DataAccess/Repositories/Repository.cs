using Microsoft.EntityFrameworkCore;
using SmartPost.DataAccess.Data;
using SmartPost.DataAccess.Interfaces;
using SmartPost.Domain.Commons;
using SmartPost.Domain.Entities;

namespace SmartPost.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
    {
        protected readonly AppDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }


        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            var entry = await _dbSet.AddAsync(entity);

            await _dbContext.SaveChangesAsync();

            return entry.Entity;
        }


        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id.Equals(id));
            _dbSet.Remove(entity);

            return await _dbContext.SaveChangesAsync() > 0;
        }


        public IQueryable<TEntity> SelectAll()
            => _dbSet;

        public async Task<TEntity> SelectByIdAsync(long id)
            => await _dbSet.FirstOrDefaultAsync(e => e.Id.Equals(id));

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var entry = _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entry.Entity;
        }
    }
}
