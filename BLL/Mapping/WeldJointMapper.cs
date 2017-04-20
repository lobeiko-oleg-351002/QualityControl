using BLL.Entities;
using BLL.Mapping.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class WeldJointMapper : IWeldJointMapper
    {
        public WeldJointMapper()
        {

        }

        public DalWeldJoint MapToDal(BllWeldJoint entity)
        {
            DalWeldJoint dalEntity = new DalWeldJoint
            {
                Id = entity.Id,
                Description = entity.Description,
                Image = entity.Image,
                Name = entity.Name

            };

            return dalEntity;
        }

        public BllWeldJoint MapToBll(DalWeldJoint entity)
        {
            BllWeldJoint bllEntity = new BllWeldJoint
            {
                Id = entity.Id,
                Description = entity.Description,
                Image = entity.Image,
                Name = entity.Name
            };

            return bllEntity;
        }
    }
}
