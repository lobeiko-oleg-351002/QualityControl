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
    public class ControlNameLibRepository : Repository<DalControlNameLib, ControlNameLib>, IControlNameLibRepository
    {
        private readonly ServiceDB context;
        public ControlNameLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        ControlNameLibMapper mapper = new ControlNameLibMapper();

        public new void Delete(DalControlNameLib entity)
        {
            var ormEntity = context.Set<ControlNameLib>().Single(ControlNameLib => ControlNameLib.id == entity.Id);
            context.Set<ControlNameLib>().Remove(ormEntity);
        }

        public new DalControlNameLib Get(int id)
        {
            var ormEntity = context.Set<ControlNameLib>().FirstOrDefault(ControlNameLib => ControlNameLib.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalControlNameLib> GetAll()
        {
            var elements = context.Set<ControlNameLib>().Select(ControlNameLib => ControlNameLib);
            var retElemets = new List<DalControlNameLib>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalControlNameLib entity)
        {
            var ormEntity = context.Set<ControlNameLib>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new ControlNameLib Create(DalControlNameLib entity)
        {
            var res = context.Set<ControlNameLib>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
