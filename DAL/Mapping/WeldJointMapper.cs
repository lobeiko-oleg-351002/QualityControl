using DAL.Entities;
using DAL.Mapping.Interfaces;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping
{
    public class WeldJointMapper : IWeldJointMapper
    {
        public DalWeldJoint MapToDal(WeldJoint entity)
        {
            return new DalWeldJoint
            {
                Id = entity.id,
                Description = entity.description,
                Image = entity.image,
                Name = entity.name
            };
        }

        public WeldJoint MapToOrm(DalWeldJoint entity)
        {
            return new WeldJoint
            {
                id = entity.Id,
                description = entity.Description,
                image = entity.Image,
                name = entity.Name
            };
        }
    }
}
