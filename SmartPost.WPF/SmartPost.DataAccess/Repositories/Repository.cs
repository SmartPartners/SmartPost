using Microsoft.EntityFrameworkCore;
using SmartPost.DataAccess.Data;
using SmartPost.DataAccess.Interfaces;
using SmartPost.Domain.Entities;

namespace SmartPost.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private DbSet<TEntity> _dbSet;

        public Repository(AppDb app)
        {
            this._dbSet = app.Set<TEntity>();
        }
        public void Add(TEntity entity) => _dbSet.Add(entity);

        public void Delete(TEntity entity, Guid id)
        {
            var model = _dbSet.Remove(entity);
        }

        public IEnumerable<TEntity> GetAll() => _dbSet;

        public TEntity GetById(Guid id)
            => _dbSet.FirstOrDefault(x => x.id == id)!;

        public void Update(TEntity entity, Guid id)
           => _dbSet.Update(entity);
    }
}
