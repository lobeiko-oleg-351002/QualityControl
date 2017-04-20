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
    public class TemplateRepository : Repository<DalTemplate, Template>, ITemplateRepository
    {
        private readonly ServiceDB context;
        public TemplateRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalTemplate GetTemplateByName(string name)
        {
            var ormEntity = context.Templates.FirstOrDefault(entity => entity.name == name);
            return mapper.MapToDal(ormEntity);
        }

        TemplateMapper mapper = new TemplateMapper();

        public new void Delete(DalTemplate entity)
        {
            var ormEntity = context.Set<Template>().Single(Template => Template.id == entity.Id);
            context.Set<Template>().Remove(ormEntity);
        }

        public new DalTemplate Get(int id)
        {
            var ormEntity = context.Set<Template>().FirstOrDefault(Template => Template.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalTemplate> GetAll()
        {
            var elements = context.Set<Template>().Select(Template => Template);
            var retElemets = new List<DalTemplate>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalTemplate entity)
        {
            var ormEntity = context.Set<Template>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new Template Create(DalTemplate entity)
        {
            var res = context.Set<Template>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
