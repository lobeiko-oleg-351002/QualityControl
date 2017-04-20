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
    public class WeldJointRepository : Repository<DalWeldJoint, WeldJoint>, IWeldJointRepository
    {
        private readonly ServiceDB context;
        public WeldJointRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalWeldJoint GetWeldJointByName(string name)
        {
            var ormEntity = context.WeldJoints.FirstOrDefault(entity => entity.name == name);
            return mapper.MapToDal(ormEntity);
        }

        WeldJointMapper mapper = new WeldJointMapper();

        public new void Delete(DalWeldJoint entity)
        {
            var ormEntity = context.Set<WeldJoint>().Single(WeldJoint => WeldJoint.id == entity.Id);
            context.Set<WeldJoint>().Remove(ormEntity);
        }

        public new DalWeldJoint Get(int id)
        {
            var ormEntity = context.Set<WeldJoint>().FirstOrDefault(WeldJoint => WeldJoint.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalWeldJoint> GetAll()
        {
            var elements = context.Set<WeldJoint>().Select(WeldJoint => WeldJoint);
            var retElemets = new List<DalWeldJoint>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalWeldJoint entity)
        {
            var ormEntity = context.Set<WeldJoint>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new WeldJoint Create(DalWeldJoint entity)
        {
            var res = context.Set<WeldJoint>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
