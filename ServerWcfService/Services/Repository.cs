using AutoMapper;
using BLL.Entities.Interface;
using BLL.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities.Interface;

namespace ServerWcfService.Services
{
    public class Repository<T, U> 
            where T : class, IUilEntity
            where U : class, IBllEntity
    {
        private readonly IService<U> service;
        public Repository(IService<U> service)
        {
            this.service = service;
        }

        public virtual void Create(T entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<T, U>();
            });
            service.Create(Mapper.Map<U>(entity));
        }

        public virtual void Delete(T entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<T, U>();
            });
            service.Delete(Mapper.Map<U>(entity));
        }

        public virtual IEnumerable<T> GetAll()
        {
            var elements = service.GetAll();
            var retElemets = new List<T>();
            foreach (var element in elements)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<U, T>();
                });
                retElemets.Add(Mapper.Map<T>(element));
            }
            return retElemets;
        }

        public virtual T Get(int id)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<U, T>();
            });
            var retElement = service.Get(id);
            return Mapper.Map<T>(retElement);
        }

        public virtual void Update(T entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<T, U>();
            });
            service.Update(Mapper.Map<U>(entity));
        }
    }
}
