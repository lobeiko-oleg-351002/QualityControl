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
    public class ControlMethodDocumentationLibRepository : Repository<DalControlMethodDocumentationLib, ControlMethodDocumentationLib>, IControlMethodDocumentationLibRepository
    {
        private readonly ServiceDB context;
        public ControlMethodDocumentationLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        ControlMethodDocumentationLibMapper mapper = new ControlMethodDocumentationLibMapper();

        public new void Delete(DalControlMethodDocumentationLib entity)
        {
            var ormEntity = context.Set<ControlMethodDocumentationLib>().Single(ControlMethodDocumentationLib => ControlMethodDocumentationLib.id == entity.Id);
            context.Set<ControlMethodDocumentationLib>().Remove(ormEntity);
        }

        public new DalControlMethodDocumentationLib Get(int id)
        {
            var ormEntity = context.Set<ControlMethodDocumentationLib>().FirstOrDefault(ControlMethodDocumentationLib => ControlMethodDocumentationLib.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalControlMethodDocumentationLib> GetAll()
        {
            var elements = context.Set<ControlMethodDocumentationLib>().Select(ControlMethodDocumentationLib => ControlMethodDocumentationLib);
            var retElemets = new List<DalControlMethodDocumentationLib>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalControlMethodDocumentationLib entity)
        {
            var ormEntity = context.Set<ControlMethodDocumentationLib>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new ControlMethodDocumentationLib Create(DalControlMethodDocumentationLib entity)
        {
            var res = context.Set<ControlMethodDocumentationLib>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
