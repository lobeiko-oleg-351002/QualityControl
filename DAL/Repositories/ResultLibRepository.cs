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
    public class ResultLibRepository : Repository<DalResultLib, ResultLib>, IResultLibRepository
    {
        private readonly ServiceDB context;
        public ResultLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public new ResultLib Create(DalResultLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DalResultLib, ResultLib>();
                cfg.CreateMap<ResultLib, DalResultLib>();
            });
            var res = context.Set<ResultLib>().Add(Mapper.Map<ResultLib>(entity));
            return res;
        }
    }
}