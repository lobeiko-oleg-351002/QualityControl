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
    public class ControlNameLibMapper : IControlNameLibMapper
    {
        public DalControlNameLib MapToDal(ControlNameLib entity)
        {
            return new DalControlNameLib
            {
                Id = entity.id,
            };
        }

        public ControlNameLib MapToOrm(DalControlNameLib entity)
        {
            return new ControlNameLib
            {
                id = entity.Id
            };
        }
    }
}
