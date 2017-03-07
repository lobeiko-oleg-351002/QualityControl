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
    public class SelectedComponentRepository : Repository<DalSelectedComponent, SelectedComponent>, ISelectedComponentRepository
    {
        private readonly ServiceDB context;
        public SelectedComponentRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<DalSelectedComponent> GetComponentsByLibId(int id)
        {
            Mapper.CreateMap<SelectedComponent, DalSelectedComponent>();
            var elements = context.Set<SelectedComponent>().Where(entity => entity.componentLib_id == id);
            var retElemets = new List<DalSelectedComponent>();
            foreach (var element in elements)
            {
                Mapper.CreateMap<SelectedComponent, DalSelectedComponent>();
                retElemets.Add(Mapper.Map<DalSelectedComponent>(element));
            }
            return retElemets;
        }
    }
}
