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
    public class ComponentLibMapper : IComponentLibMapper
    {
        public DalComponentLib MapToDal(ComponentLib entity)
        {
            return new DalComponentLib
            {
                Id = entity.id,
            };
        }

        public ComponentLib MapToOrm(DalComponentLib entity)
        {
            return new ComponentLib
            {
                id = entity.Id
            };
        }
    }
}
