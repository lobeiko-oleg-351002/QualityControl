using AutoMapper;
using BLL.Entities.Interface;
using BLL.Services.Interface;
using DAL.Entities.Interface;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class Service<T, U> : IService<T>
        where T : class, IBllEntity
        where U : class, IDalEntity
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<U> repository;
        public Service(IUnitOfWork uow, IRepository<U> repository)
        {
            this.uow = uow;
            this.repository = repository;
        }

        public virtual void Create(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual T Get(int id)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
