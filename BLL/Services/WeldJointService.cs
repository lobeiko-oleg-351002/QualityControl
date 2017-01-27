using AutoMapper;
using BLL.Entities;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class WeldJointService : Service<BllWeldJoint, DalWeldJoint>, IWeldJointService
    {
        private readonly IUnitOfWork uow;

        public WeldJointService(IUnitOfWork uow) : base(uow, uow.WeldJoints)
        {
            this.uow = uow;
        }

        public BllWeldJoint GetWeldJointByName(string name)
        {
            Mapper.CreateMap<DalWeldJoint, BllWeldJoint>();
            return Mapper.Map<BllWeldJoint>(uow.WeldJoints.GetWeldJointByName(name)); 
        }
    }
}
