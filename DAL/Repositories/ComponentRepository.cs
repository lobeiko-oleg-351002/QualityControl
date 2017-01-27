using AutoMapper;
using DAL.Entities;
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
            Mapper.CreateMap<Component, DalComponent>();
            var ormEntity = context.Components.FirstOrDefault(entity => entity.name == name);
            return Mapper.Map<DalComponent>(ormEntity);
        }

        public IEnumerable<DalComponent> GetComponentsByTemplateId(int id)
        {
            Mapper.CreateMap<Component, DalComponent>();
            var elements = context.Components.Select(entity => entity.template_id == id);
            var retElemets = new List<DalComponent>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<DalComponent>(element));
            }
            return retElemets;
        }
    }
}
