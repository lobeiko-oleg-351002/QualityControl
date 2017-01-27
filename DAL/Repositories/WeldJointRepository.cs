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
    public class WeldJointRepository : Repository<DalWeldJoint, WeldJoint>, IWeldJointRepository
    {
        private readonly ServiceDB context;
        public WeldJointRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalWeldJoint GetWeldJointByName(string name)
        {
            Mapper.CreateMap<WeldJoint, DalWeldJoint>();
            var ormEntity = context.WeldJoints.FirstOrDefault(entity => entity.name == name);
            return Mapper.Map<DalWeldJoint>(ormEntity);
        }
    }
}
