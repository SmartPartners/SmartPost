using SmartPost.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPost.DataAccess.Interfaces
{
    public interface IRepository<TEntity> 
        where TEntity : BaseEntity 
    {
        void Add(TEntity entity);
        void Delete(TEntity entity, Guid id);
        void Update(TEntity entity, Guid id);
        IEnumerable<TEntity> GetAll();
        TEntity GetById(Guid id);
    }
}
