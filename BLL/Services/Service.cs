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
            Mapper.CreateMap<T, U>();
            repository.Create(Mapper.Map<U>(entity));
            uow.Commit();
        }

        public virtual void Delete(T entity)
        {
            Mapper.CreateMap<T, U>();
            repository.Delete(Mapper.Map<U>(entity));
            uow.Commit();
        }

        public virtual IEnumerable<T> GetAll()
        {
            Mapper.CreateMap<U, T>();
            var elements = repository.GetAll();
            var retElemets = new List<T>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<T>(element));
            }
            return retElemets;
        }

        public virtual T Get(int id)
        {
            Mapper.CreateMap<U, T>();
            var retElement = repository.Get(id);
            return Mapper.Map<T>(retElement);
        }

        public virtual void Update(T entity)
        {
            Mapper.CreateMap<T, U>();
            repository.Update(Mapper.Map<U>(entity));
            uow.Commit();
        }
    }
}
