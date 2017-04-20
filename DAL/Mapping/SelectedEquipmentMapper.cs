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
    public class SelectedEquipmentMapper : ISelectedEquipmentMapper
    {
        public DalSelectedEquipment MapToDal(SelectedEquipment entity)
        {
            return new DalSelectedEquipment
            {
                Id = entity.id,
                EquipmentLib_id = entity.equipmentLib_id,
                Equipment_id = entity.equipment_id
            };
        }

        public SelectedEquipment MapToOrm(DalSelectedEquipment entity)
        {
            return new SelectedEquipment
            {
                id = entity.Id,
                equipment_id = entity.Equipment_id,
                equipmentLib_id = entity.EquipmentLib_id
            };
        }
    }
}
