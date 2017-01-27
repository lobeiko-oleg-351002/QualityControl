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
    public class ControlMethodsLibRepository : Repository<DalControlMethodsLib, ControlMethodsLib>, IControlMethodsLibRepository
    {
        private readonly ServiceDB context;
        public ControlMethodsLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public new ControlMethodsLib Create(DalControlMethodsLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DalControlMethodsLib, ControlMethodsLib>();
                cfg.CreateMap<ControlMethodsLib, DalControlMethodsLib>();
            });
            var res = context.Set<ControlMethodsLib>().Add(Mapper.Map<ControlMethodsLib>(entity));
            return res;
        }
    }
}
