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
    public class ControlMethodDocumentationRepository : Repository<DalControlMethodDocumentation, ControlMethodDocumentation>, IControlMethodDocumentationRepository
    {
        private readonly ServiceDB context;
        public ControlMethodDocumentationRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalControlMethodDocumentation GetControlMethodDocumentationByName(string name)
        {
            Mapper.CreateMap<ControlMethodDocumentation, DalControlMethodDocumentation>();
            var ormEntity = context.ControlMethodDocumentations.FirstOrDefault(entity => entity.name == name);
            return Mapper.Map<DalControlMethodDocumentation>(ormEntity);
        }
    }
}
