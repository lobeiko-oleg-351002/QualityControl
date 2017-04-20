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
    public class SelectedComponentRepository : Repository<DalSelectedComponent, SelectedComponent>, ISelectedComponentRepository
    {
        private readonly ServiceDB context;
        public SelectedComponentRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<DalSelectedComponent> GetComponentsByLibId(int id)
        {
            var elements = context.Set<SelectedComponent>().Where(entity => entity.componentLib_id == id);
            var retElemets = new List<DalSelectedComponent>();
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapToDal(element));
            }
            return retElemets;
        }

        SelectedComponentMapper mapper = new SelectedComponentMapper();

        public new void Delete(DalSelectedComponent entity)
        {
            var ormEntity = context.Set<SelectedComponent>().Single(SelectedComponent => SelectedComponent.id == entity.Id);
            context.Set<SelectedComponent>().Remove(ormEntity);
        }

        public new DalSelectedComponent Get(int id)
        {
            var ormEntity = context.Set<SelectedComponent>().FirstOrDefault(SelectedComponent => SelectedComponent.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalSelectedComponent> GetAll()
        {
            var elements = context.Set<SelectedComponent>().Select(SelectedComponent => SelectedComponent);
            var retElemets = new List<DalSelectedComponent>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalSelectedComponent entity)
        {
            var ormEntity = context.Set<SelectedComponent>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new SelectedComponent Create(DalSelectedComponent entity)
        {
            var res = context.Set<SelectedComponent>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
