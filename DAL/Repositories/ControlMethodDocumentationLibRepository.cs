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
    public class ControlMethodDocumentationLibRepository : Repository<DalControlMethodDocumentationLib, ControlMethodDocumentationLib>, IControlMethodDocumentationLibRepository
    {
        private readonly ServiceDB context;
        public ControlMethodDocumentationLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public new ControlMethodDocumentationLib Create(DalControlMethodDocumentationLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DalControlMethodDocumentationLib, ControlMethodDocumentationLib>();
                cfg.CreateMap<ControlMethodDocumentationLib, DalControlMethodDocumentationLib>();
            });
            var res = context.Set<ControlMethodDocumentationLib>().Add(Mapper.Map<ControlMethodDocumentationLib>(entity));
            return res;
        }
    }
}
