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
    public class RequirementDocumentationLibRepository : Repository<DalRequirementDocumentationLib, RequirementDocumentationLib>, IRequirementDocumentationLibRepository
    {
        private readonly ServiceDB context;
        public RequirementDocumentationLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        RequirementDocumentationLibMapper mapper = new RequirementDocumentationLibMapper();

        public new void Delete(DalRequirementDocumentationLib entity)
        {
            var ormEntity = context.Set<RequirementDocumentationLib>().Single(RequirementDocumentationLib => RequirementDocumentationLib.id == entity.Id);
            context.Set<RequirementDocumentationLib>().Remove(ormEntity);
        }

        public new DalRequirementDocumentationLib Get(int id)
        {
            var ormEntity = context.Set<RequirementDocumentationLib>().FirstOrDefault(RequirementDocumentationLib => RequirementDocumentationLib.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalRequirementDocumentationLib> GetAll()
        {
            var elements = context.Set<RequirementDocumentationLib>().Select(RequirementDocumentationLib => RequirementDocumentationLib);
            var retElemets = new List<DalRequirementDocumentationLib>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalRequirementDocumentationLib entity)
        {
            var ormEntity = context.Set<RequirementDocumentationLib>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new RequirementDocumentationLib Create(DalRequirementDocumentationLib entity)
        {
            var res = context.Set<RequirementDocumentationLib>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
