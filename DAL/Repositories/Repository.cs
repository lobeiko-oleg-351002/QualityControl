using DAL.Entities.Interface;
using DAL.Repositories.Interface;
using ORM;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System;

namespace DAL.Repositories
{
    public class Repository<TEntity, UEntity> : IRepository<TEntity>
        where TEntity : class, IDalEntity
        where UEntity : class, IOrmEntity
    {
        private readonly ServiceDB context;


        public Repository(ServiceDB context)
        {
            this.context = context;
        }

        public void Create(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
