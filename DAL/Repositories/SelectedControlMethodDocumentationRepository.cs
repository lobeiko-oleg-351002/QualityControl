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
    public class SelectedControlMethodDocumentationRepository : Repository<DalSelectedControlMethodDocumentation, SelectedControlMethodDocumentation>, ISelectedControlMethodDocumentationRepository
    {
        private readonly ServiceDB context;
        public SelectedControlMethodDocumentationRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<DalSelectedControlMethodDocumentation> GetControlMethodDocumentationsByLibId(int id)
        {
            var elements = context.Set<SelectedControlMethodDocumentation>().Where(entity => entity.controlMethodDocumentationLib_id == id);
            var retElemets = new List<DalSelectedControlMethodDocumentation>();
            foreach (var element in elements)
            {
                 retElemets.Add(mapper.MapToDal(element));
            }
            return retElemets;
        }

        public new SelectedControlMethodDocumentation Create(DalSelectedControlMethodDocumentation entity)
        {
            var ormEntity = mapper.MapToOrm(entity);
            ormEntity.ControlMethodDocumentationLib = context.ControlMethodDocumentationLibs.FirstOrDefault(e => e.id == ormEntity.controlMethodDocumentationLib_id);
            //ormEntity.SelectedControlMethodDocumentationLib.SelectedControlMethodDocumentation.Add(ormEntity);
            return context.Set<SelectedControlMethodDocumentation>().Add(ormEntity);
        }

        SelectedControlMethodDocumentationMapper mapper = new SelectedControlMethodDocumentationMapper();

        public new void Delete(DalSelectedControlMethodDocumentation entity)
        {
            var ormEntity = context.Set<SelectedControlMethodDocumentation>().Single(SelectedControlMethodDocumentation => SelectedControlMethodDocumentation.id == entity.Id);
            context.Set<SelectedControlMethodDocumentation>().Remove(ormEntity);
        }

        public new DalSelectedControlMethodDocumentation Get(int id)
        {
            var ormEntity = context.Set<SelectedControlMethodDocumentation>().FirstOrDefault(SelectedControlMethodDocumentation => SelectedControlMethodDocumentation.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalSelectedControlMethodDocumentation> GetAll()
        {
            var elements = context.Set<SelectedControlMethodDocumentation>().Select(SelectedControlMethodDocumentation => SelectedControlMethodDocumentation);
            var retElemets = new List<DalSelectedControlMethodDocumentation>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalSelectedControlMethodDocumentation entity)
        {
            var ormEntity = context.Set<SelectedControlMethodDocumentation>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

    }
}
