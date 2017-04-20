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
    public class EquipmentMapper : IEquipmentMapper
    {

        public EquipmentMapper()
        {

        }

        public DalEquipment MapToDal(BllEquipment entity)
        {
            DalEquipment dalEntity = new DalEquipment
            {
                Id = entity.Id,
                CheckDate = entity.CheckDate,
                IsChecked = entity.IsChecked.Value,
                FactoryNumber= entity.FactoryNumber,
                Name = entity.Name,
                NextTechnicalCheckDate = entity.NextTechnicalCheckDate,
                NumberOfTechnicalCheck = entity.NumberOfTechnicalCheck,
                Pressmark = entity.Pressmark,
                TechnicalCheckDate = entity.TechnicalCheckDate,
                Type = entity.Type

            };

            return dalEntity;
        }

        public BllEquipment MapToBll(DalEquipment entity)
        {
            BllEquipment bllEntity = new BllEquipment
            {
                Id = entity.Id,
                CheckDate = entity.CheckDate,
                IsChecked = entity.IsChecked,
                FactoryNumber = entity.FactoryNumber,
                Name = entity.Name,
                NextTechnicalCheckDate = entity.NextTechnicalCheckDate,
                NumberOfTechnicalCheck = entity.NumberOfTechnicalCheck,
                Pressmark = entity.Pressmark,
                TechnicalCheckDate = entity.TechnicalCheckDate,
                Type = entity.Type
            };

            return bllEntity;
        }
    }
}
