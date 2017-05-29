using DAL.Entities.Interface;
using System.Collections.Generic;

namespace DAL.Repositories.Interface
{
    public interface IRepository<TEntity> where TEntity : IDalEntity
    {
        void Create(TEntity entity);

        IEnumerable<TEntity> GetAll();

        TEntity Get(int id);

        void Delete(TEntity entity);

        void Update(TEntity entity);
    }
}
