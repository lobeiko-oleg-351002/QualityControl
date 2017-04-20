using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
using BLL.Mapping.Interfaces;
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
        IWeldJointMapper bllMapper = new WeldJointMapper();
        public WeldJointService(IUnitOfWork uow) : base(uow, uow.WeldJoints)
        {
            this.uow = uow;
        }

        public override void Create(BllWeldJoint entity)
        {
            uow.WeldJoints.Create(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Update(BllWeldJoint entity)
        {
            uow.WeldJoints.Update(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllWeldJoint entity)
        {
            uow.WeldJoints.Delete(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override IEnumerable<BllWeldJoint> GetAll()
        {
            var elements = uow.WeldJoints.GetAll();
            var retElemets = new List<BllWeldJoint>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }

        public override BllWeldJoint Get(int id)
        {
            return bllMapper.MapToBll(uow.WeldJoints.Get(id));
        }

        public BllWeldJoint GetWeldJointByName(string name)
        {
            return bllMapper.MapToBll(uow.WeldJoints.GetWeldJointByName(name)); 
        }
    }
}
