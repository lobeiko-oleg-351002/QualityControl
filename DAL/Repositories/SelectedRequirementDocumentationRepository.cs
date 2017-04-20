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
    public class SelectedRequirementDocumentationRepository : Repository<DalSelectedRequirementDocumentation, SelectedRequirementDocumentation>, ISelectedRequirementDocumentationRepository
    {
        private readonly ServiceDB context;
        public SelectedRequirementDocumentationRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<DalSelectedRequirementDocumentation> GetRequirementDocumentationsByLibId(int id)
        {
            var elements = context.Set<SelectedRequirementDocumentation>().Where(entity => entity.requirementDocumentationLib_id == id);
            var retElemets = new List<DalSelectedRequirementDocumentation>();
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapToDal(element));
            }
            return retElemets;
        }

        public new SelectedRequirementDocumentation Create(DalSelectedRequirementDocumentation entity)
        {
            var ormEntity = mapper.MapToOrm(entity);
            ormEntity.RequirementDocumentationLib = context.RequirementDocumentationLibs.FirstOrDefault(e => e.id == ormEntity.requirementDocumentationLib_id);
            return context.Set<SelectedRequirementDocumentation>().Add(ormEntity);
        }

        SelectedRequirementDocumentationMapper mapper = new SelectedRequirementDocumentationMapper();

        public new void Delete(DalSelectedRequirementDocumentation entity)
        {
            var ormEntity = context.Set<SelectedRequirementDocumentation>().Single(SelectedRequirementDocumentation => SelectedRequirementDocumentation.id == entity.Id);
            context.Set<SelectedRequirementDocumentation>().Remove(ormEntity);
        }

        public new DalSelectedRequirementDocumentation Get(int id)
        {
            var ormEntity = context.Set<SelectedRequirementDocumentation>().FirstOrDefault(SelectedRequirementDocumentation => SelectedRequirementDocumentation.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalSelectedRequirementDocumentation> GetAll()
        {
            var elements = context.Set<SelectedRequirementDocumentation>().Select(SelectedRequirementDocumentation => SelectedRequirementDocumentation);
            var retElemets = new List<DalSelectedRequirementDocumentation>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalSelectedRequirementDocumentation entity)
        {
            var ormEntity = context.Set<SelectedRequirementDocumentation>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

    }
}
