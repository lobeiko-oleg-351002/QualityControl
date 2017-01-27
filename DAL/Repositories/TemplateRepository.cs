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
    public class TemplateRepository : Repository<DalTemplate, Template>, ITemplateRepository
    {
        private readonly ServiceDB context;
        public TemplateRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalTemplate GetTemplateByName(string name)
        {
            Mapper.CreateMap<Template, DalTemplate>();
            var ormEntity = context.Templates.FirstOrDefault(entity => entity.name == name);
            return Mapper.Map<DalTemplate>(ormEntity);
        }
    }
}
