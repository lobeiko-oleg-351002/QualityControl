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
    public class ComponentLibRepository : Repository<DalComponentLib, ComponentLib>, IComponentLibRepository
    {
        private readonly ServiceDB context;
        public ComponentLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public new ComponentLib Create(DalComponentLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DalComponentLib, ComponentLib>();
                cfg.CreateMap<ComponentLib, DalComponentLib>();
            });
            var res = context.Set<ComponentLib>().Add(Mapper.Map<ComponentLib>(entity));
            return res;
        }
    }
}
