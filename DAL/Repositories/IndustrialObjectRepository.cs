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
    public class IndustrialObjectRepository: Repository<DalIndustrialObject, IndustrialObject>, IIndustrialObjectRepository
    {
        private readonly ServiceDB context;
        public IndustrialObjectRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalIndustrialObject GetIndustrialObjectByName(string name)
        {
            var ormEntity = context.IndustrialObjects.FirstOrDefault(entity => entity.name == name);
            return mapper.MapToDal(ormEntity);
        }

        IndustrialObjectMapper mapper = new IndustrialObjectMapper();

        public new void Delete(DalIndustrialObject entity)
        {
            var ormEntity = context.Set<IndustrialObject>().Single(IndustrialObject => IndustrialObject.id == entity.Id);
            context.Set<IndustrialObject>().Remove(ormEntity);
        }

        public new DalIndustrialObject Get(int id)
        {
            var ormEntity = context.Set<IndustrialObject>().FirstOrDefault(IndustrialObject => IndustrialObject.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalIndustrialObject> GetAll()
        {
            var elements = context.Set<IndustrialObject>().Select(IndustrialObject => IndustrialObject);
            var retElemets = new List<DalIndustrialObject>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalIndustrialObject entity)
        {
            var ormEntity = context.Set<IndustrialObject>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new IndustrialObject Create(DalIndustrialObject entity)
        {
            var res = context.Set<IndustrialObject>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
