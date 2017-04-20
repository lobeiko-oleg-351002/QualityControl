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
    public class ComponentRepository : Repository<DalComponent, Component>, IComponentRepository
    {
        private readonly ServiceDB context;
        public ComponentRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalComponent GetComponentByName(string name)
        {
            var ormEntity = context.Components.FirstOrDefault(entity => entity.name == name);
            return mapper.MapToDal(ormEntity);
        }

        public IEnumerable<DalComponent> GetComponentsByTemplateId(int id)
        {
            ComponentMapper mapper = new ComponentMapper();
            var elements = context.Components.Select(entity => entity.template_id == id);
            var retElemets = new List<DalComponent>();
            //foreach (var element in elements)
            //{
            //    retElemets.Add(mapper.MapToDal(element));
            //}
            return retElemets;
        }

        ComponentMapper mapper = new ComponentMapper();

        public new void Delete(DalComponent entity)
        {
            var ormEntity = context.Set<Component>().Single(Component => Component.id == entity.Id);
            context.Set<Component>().Remove(ormEntity);
        }

        public new DalComponent Get(int id)
        {
            var ormEntity = context.Set<Component>().FirstOrDefault(Component => Component.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalComponent> GetAll()
        {
            var elements = context.Set<Component>().Select(Component => Component);
            var retElemets = new List<DalComponent>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalComponent entity)
        {
            var ormEntity = context.Set<Component>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new Component Create(DalComponent entity)
        {
            var res = context.Set<Component>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
