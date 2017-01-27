using DAL.Entities.Interface;
using DAL.Repositories.Interface;
using ORM;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ORM.Interface;

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

        public virtual void Create(TEntity entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<TEntity, UEntity>();
            });
            var ormEntity = Mapper.Map<UEntity>(entity);
            context.Set<UEntity>().Add(ormEntity);
        }

        public void Delete(TEntity entity)
        {
            var ormEntity = context.Set<UEntity>().Single(uentity => uentity.id == entity.Id);
            context.Set<UEntity>().Remove(ormEntity);
        }

        public TEntity Get(int id)
        {
            Mapper.CreateMap<UEntity, TEntity>();
            var ormEntity = context.Set<UEntity>().FirstOrDefault(uentity => uentity.id == id);
            return ormEntity != null ? (Mapper.Map<TEntity>(ormEntity)) : null;
        }

        public IEnumerable<TEntity> GetAll()
        {
            Mapper.CreateMap<UEntity, TEntity>();
            var elements = context.Set<UEntity>().Select(uentity => uentity);
            var retElemets = new List<TEntity>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(Mapper.Map<TEntity>(element));
                }
            }

            return retElemets;
        }

        public void Update(TEntity entity)
        {
            var ormEntity = context.Set<UEntity>().Find(entity.Id);
            Mapper.CreateMap<TEntity, UEntity>();
            if (ormEntity != null)
            {                
                context.Entry(ormEntity).CurrentValues.SetValues(Mapper.Map<UEntity>(entity));
            }
        }
    }
}
