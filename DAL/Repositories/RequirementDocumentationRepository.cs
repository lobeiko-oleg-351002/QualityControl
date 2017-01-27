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
    public class RequirementDocumentationRepository : Repository<DalRequirementDocumentation,RequirementDocumentation>, IRequirementDocumentationRepository
    {
        private readonly ServiceDB context;
        public RequirementDocumentationRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalRequirementDocumentation GetRequirementDocumentationByName(string name)
        {
            Mapper.CreateMap<RequirementDocumentation, DalRequirementDocumentation>();
            var ormEntity = context.RequirementDocumentations.FirstOrDefault(entity => entity.name == name);
            return Mapper.Map<DalRequirementDocumentation>(ormEntity);
        }
    }
}
