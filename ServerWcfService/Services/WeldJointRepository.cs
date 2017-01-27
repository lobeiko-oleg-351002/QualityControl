using AutoMapper;
using BLL.Entities;
using BLL.Services.Interface;
using ServerWcfService.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities;

namespace ServerWcfService.Services
{
    public class WeldJointRepository : Repository<UilWeldJoint, BllWeldJoint>, IWeldJointRepository
    {
        private readonly IWeldJointService weldJointService;

        public WeldJointRepository() : base(UiUnitOfWork.Instance.WeldJoints)
        {
            weldJointService = UiUnitOfWork.Instance.WeldJoints;
        }

        public UilWeldJoint GetWeldJointByName(string name)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllWeldJoint, UilWeldJoint>();
            });
            return Mapper.Map<UilWeldJoint>(weldJointService.GetWeldJointByName(name));
        }
    }
    
}
