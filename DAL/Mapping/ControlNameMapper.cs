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
    public class ControlNameMapper : IControlNameMapper
    {
        public DalControlName MapToDal(ControlName entity)
        {
            return new DalControlName
            {
                Id = entity.id,
                Name = entity.name
            };
        }

        public ControlName MapToOrm(DalControlName entity)
        {
            return new ControlName
            {
                id = entity.Id,
                name = entity.Name
            };
        }
    }
}
