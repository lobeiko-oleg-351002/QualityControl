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
    public class EquipmentMapper : IEquipmentMapper
    {
        public DalEquipment MapToDal(Equipment entity)
        {
            return new DalEquipment
            {
                Id = entity.id,
                CheckDate = entity.checkDate,
                IsChecked = entity.isChecked.Value,
                NextTechnicalCheckDate = entity.nextTechnicalCheckDate,
                NumberOfTechnicalCheck = entity.numberOfTechnicalCheck,
                TechnicalCheckDate = entity.technicalCheckDate,
                FactoryNumber = entity.factoryNumber,
                Name = entity.name,
                Pressmark = entity.pressmark,
                Type = entity.type,
            };
        }

        public Equipment MapToOrm(DalEquipment entity)
        {
            return new Equipment
            {
                id = entity.Id,
                checkDate = entity.CheckDate,
                isChecked = entity.IsChecked,
                nextTechnicalCheckDate = entity.NextTechnicalCheckDate,
                numberOfTechnicalCheck = entity.NumberOfTechnicalCheck,
                technicalCheckDate = entity.TechnicalCheckDate,
                factoryNumber = entity.FactoryNumber,
                name = entity.Name,
                pressmark = entity.Pressmark,
                type = entity.Type,
            };
        }
    }
}
