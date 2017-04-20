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
    public class ControlMethodDocumentationRepository : Repository<DalControlMethodDocumentation, ControlMethodDocumentation>, IControlMethodDocumentationRepository
    {
        private readonly ServiceDB context;
        public ControlMethodDocumentationRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalControlMethodDocumentation GetControlMethodDocumentationByName(string name)
        {
            var ormEntity = context.ControlMethodDocumentations.FirstOrDefault(entity => entity.name == name);
            return mapper.MapToDal(ormEntity);
        }

        ControlMethodDocumentationMapper mapper = new ControlMethodDocumentationMapper();

        public new void Delete(DalControlMethodDocumentation entity)
        {
            var ormEntity = context.Set<ControlMethodDocumentation>().Single(ControlMethodDocumentation => ControlMethodDocumentation.id == entity.Id);
            context.Set<ControlMethodDocumentation>().Remove(ormEntity);
        }

        public new DalControlMethodDocumentation Get(int id)
        {
            var ormEntity = context.Set<ControlMethodDocumentation>().FirstOrDefault(ControlMethodDocumentation => ControlMethodDocumentation.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalControlMethodDocumentation> GetAll()
        {
            var elements = context.Set<ControlMethodDocumentation>().Select(ControlMethodDocumentation => ControlMethodDocumentation);
            var retElemets = new List<DalControlMethodDocumentation>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalControlMethodDocumentation entity)
        {
            var ormEntity = context.Set<ControlMethodDocumentation>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new ControlMethodDocumentation Create(DalControlMethodDocumentation entity)
        {
            var res = context.Set<ControlMethodDocumentation>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
