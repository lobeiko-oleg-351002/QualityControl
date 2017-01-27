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
    public class SelectedRequirementDocumentationRepository : Repository<DalSelectedRequirementDocumentation, SelectedRequirementDocumentation>, ISelectedRequirementDocumentationRepository
    {
        private readonly ServiceDB context;
        public SelectedRequirementDocumentationRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<DalSelectedRequirementDocumentation> GetRequirementDocumentationsByLibId(int id)
        {
            Mapper.CreateMap<SelectedRequirementDocumentation, DalSelectedRequirementDocumentation>();
            var elements = context.Set<SelectedRequirementDocumentation>().Where(entity => entity.requirementDocumentationLib_id == id);
            var retElemets = new List<DalSelectedRequirementDocumentation>();
            foreach (var element in elements)
            {
                Mapper.CreateMap<SelectedRequirementDocumentation, DalSelectedRequirementDocumentation>();
                retElemets.Add(Mapper.Map<DalSelectedRequirementDocumentation>(element));
            }
            return retElemets;
        }

        public new SelectedRequirementDocumentation Create(DalSelectedRequirementDocumentation entity)
        {
            Mapper.CreateMap<DalSelectedRequirementDocumentation, SelectedRequirementDocumentation>();
            var ormEntity = Mapper.Map<SelectedRequirementDocumentation>(entity);
            ormEntity.RequirementDocumentationLib = context.RequirementDocumentationLibs.FirstOrDefault(e => e.id == ormEntity.requirementDocumentationLib_id);
            return context.Set<SelectedRequirementDocumentation>().Add(Mapper.Map<SelectedRequirementDocumentation>(entity));
        }
    }
}
