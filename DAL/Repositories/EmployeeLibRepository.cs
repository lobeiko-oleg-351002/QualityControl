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
    public class EmployeeLibRepository : Repository<DalEmployeeLib, EmployeeLib>, IEmployeeLibRepository
    {
        private readonly ServiceDB context;
        public EmployeeLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public new EmployeeLib Create(DalEmployeeLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DalEmployeeLib, EmployeeLib>();
                cfg.CreateMap<EmployeeLib, DalEmployeeLib>();
            });
            var res = context.Set<EmployeeLib>().Add(Mapper.Map<EmployeeLib>(entity));
            return res;
        }
    }
}
