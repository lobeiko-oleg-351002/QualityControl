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
    public class RequirementDocumentationLibRepository : Repository<DalRequirementDocumentationLib, RequirementDocumentationLib>, IRequirementDocumentationLibRepository
    {
        private readonly ServiceDB context;
        public RequirementDocumentationLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public new RequirementDocumentationLib Create(DalRequirementDocumentationLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DalRequirementDocumentationLib, RequirementDocumentationLib>();
                cfg.CreateMap<RequirementDocumentationLib, DalRequirementDocumentationLib>();
            });
            var res = context.Set<RequirementDocumentationLib>().Add(Mapper.Map<RequirementDocumentationLib>(entity));
            return res;
        }
    }
}
