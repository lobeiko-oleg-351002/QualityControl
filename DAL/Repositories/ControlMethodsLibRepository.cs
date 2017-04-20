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
    public class ControlMethodsLibRepository : Repository<DalControlMethodsLib, ControlMethodsLib>, IControlMethodsLibRepository
    {
        private readonly ServiceDB context;
        public ControlMethodsLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        ControlMethodsLibMapper mapper = new ControlMethodsLibMapper();

        public new void Delete(DalControlMethodsLib entity)
        {
            var ormEntity = context.Set<ControlMethodsLib>().Single(ControlMethodsLib => ControlMethodsLib.id == entity.Id);
            context.Set<ControlMethodsLib>().Remove(ormEntity);
        }

        public new DalControlMethodsLib Get(int id)
        {
            var ormEntity = context.Set<ControlMethodsLib>().FirstOrDefault(ControlMethodsLib => ControlMethodsLib.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalControlMethodsLib> GetAll()
        {
            var elements = context.Set<ControlMethodsLib>().Select(ControlMethodsLib => ControlMethodsLib);
            var retElemets = new List<DalControlMethodsLib>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalControlMethodsLib entity)
        {
            var ormEntity = context.Set<ControlMethodsLib>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new ControlMethodsLib Create(DalControlMethodsLib entity)
        {
            var res = context.Set<ControlMethodsLib>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
