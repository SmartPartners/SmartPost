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
            try
            {

                var model = _dbSet.Remove(entity);
            }
            catch
            {

            }

        }

        public IEnumerable<TEntity> GetAll() => _dbSet;


        public TEntity GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
