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
    public class EquipmentLibMapper : IEquipmentLibMapper
    {
        public DalEquipmentLib MapToDal(EquipmentLib entity)
        {
            return new DalEquipmentLib
            {
                Id = entity.id,
            };
        }

        public EquipmentLib MapToOrm(DalEquipmentLib entity)
        {
            return new EquipmentLib
            {
                id = entity.Id
            };
        }
    }
}
