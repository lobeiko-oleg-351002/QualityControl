using AutoMapper;
using DAL.Entities;
using DAL.Mapping;
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
            var ormEntity = context.RequirementDocumentations.FirstOrDefault(entity => entity.name == name);
            return mapper.MapToDal(ormEntity);
        }

        RequirementDocumentationMapper mapper = new RequirementDocumentationMapper();

        public new void Delete(DalRequirementDocumentation entity)
        {
            var ormEntity = context.Set<RequirementDocumentation>().Single(RequirementDocumentation => RequirementDocumentation.id == entity.Id);
            context.Set<RequirementDocumentation>().Remove(ormEntity);
        }

        public new DalRequirementDocumentation Get(int id)
        {
            var ormEntity = context.Set<RequirementDocumentation>().FirstOrDefault(RequirementDocumentation => RequirementDocumentation.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalRequirementDocumentation> GetAll()
        {
            var elements = context.Set<RequirementDocumentation>().Select(RequirementDocumentation => RequirementDocumentation);
            var retElemets = new List<DalRequirementDocumentation>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalRequirementDocumentation entity)
        {
            var ormEntity = context.Set<RequirementDocumentation>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new RequirementDocumentation Create(DalRequirementDocumentation entity)
        {
            var res = context.Set<RequirementDocumentation>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
