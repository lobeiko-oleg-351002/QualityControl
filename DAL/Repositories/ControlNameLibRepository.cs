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
    public class ControlNameLibRepository : Repository<DalControlNameLib, ControlNameLib>, IControlNameLibRepository
    {
        private readonly ServiceDB context;
        public ControlNameLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public new ControlNameLib Create(DalControlNameLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DalControlNameLib, ControlNameLib>();
                cfg.CreateMap<ControlNameLib, DalControlNameLib>();
            });
            var res = context.Set<ControlNameLib>().Add(Mapper.Map<ControlNameLib>(entity));
            return res;
        }
    }
}
