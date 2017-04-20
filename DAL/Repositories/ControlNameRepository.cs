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
    public class ControlNameRepository : Repository<DalControlName, ControlName>, IControlNameRepository
    {
        private readonly ServiceDB context;
        public ControlNameRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalControlName GetControlNameByName(string name)
        {
            
            var ormEntity = context.ControlNames.FirstOrDefault(entity => entity.name == name);
            return mapper.MapToDal(ormEntity);
        }

        ControlNameMapper mapper = new ControlNameMapper();

        public new void Delete(DalControlName entity)
        {
            var ormEntity = context.Set<ControlName>().Single(ControlName => ControlName.id == entity.Id);
            context.Set<ControlName>().Remove(ormEntity);
        }

        public new DalControlName Get(int id)
        {
            var ormEntity = context.Set<ControlName>().FirstOrDefault(ControlName => ControlName.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalControlName> GetAll()
        {
            var elements = context.Set<ControlName>().Select(ControlName => ControlName);
            var retElemets = new List<DalControlName>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalControlName entity)
        {
            var ormEntity = context.Set<ControlName>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new ControlName Create(DalControlName entity)
        {
            var res = context.Set<ControlName>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
