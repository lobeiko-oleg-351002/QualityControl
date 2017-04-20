using AutoMapper;
using DAL.Entities;
using DAL.Mapping;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ComponentLibRepository : Repository<DalComponentLib, ComponentLib>, IComponentLibRepository
    {
        private readonly ServiceDB context;
        public ComponentLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }


        ComponentLibMapper mapper = new ComponentLibMapper();

        public new void Delete(DalComponentLib entity)
        {
            var ormEntity = context.Set<ComponentLib>().Single(ComponentLib => ComponentLib.id == entity.Id);
            context.Set<ComponentLib>().Remove(ormEntity);
        }

        public new DalComponentLib Get(int id)
        {
            var ormEntity = context.Set<ComponentLib>().FirstOrDefault(ComponentLib => ComponentLib.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalComponentLib> GetAll()
        {
            var elements = context.Set<ComponentLib>().Select(ComponentLib => ComponentLib);
            var retElemets = new List<DalComponentLib>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalComponentLib entity)
        {
            var ormEntity = context.Set<ComponentLib>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new ComponentLib Create(DalComponentLib entity)
        {
            var res = context.Set<ComponentLib>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
