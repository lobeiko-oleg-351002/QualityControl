using AutoMapper;
using DAL.Entities;
using DAL.Mapping;
using DAL.Repositories;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SelectedControlNameRepository : Repository<DalSelectedControlName, SelectedControlName>, ISelectedControlNameRepository
    {
        private readonly ServiceDB context;
        public SelectedControlNameRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<DalSelectedControlName> GetControlNamesByLibId(int id)
        {
            var elements = context.Set<SelectedControlName>().Where(entity => entity.controlNameLib_id == id);
            var retElemets = new List<DalSelectedControlName>();
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapToDal(element));
            }
            return retElemets;
        }

        SelectedControlNameMapper mapper = new SelectedControlNameMapper();

        public new void Delete(DalSelectedControlName entity)
        {
            var ormEntity = context.Set<SelectedControlName>().Single(SelectedControlName => SelectedControlName.id == entity.Id);
            context.Set<SelectedControlName>().Remove(ormEntity);
        }

        public new DalSelectedControlName Get(int id)
        {
            var ormEntity = context.Set<SelectedControlName>().FirstOrDefault(SelectedControlName => SelectedControlName.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalSelectedControlName> GetAll()
        {
            var elements = context.Set<SelectedControlName>().Select(SelectedControlName => SelectedControlName);
            var retElemets = new List<DalSelectedControlName>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalSelectedControlName entity)
        {
            var ormEntity = context.Set<SelectedControlName>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new SelectedControlName Create(DalSelectedControlName entity)
        {
            var res = context.Set<SelectedControlName>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
