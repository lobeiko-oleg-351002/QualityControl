using BLL.Entities;
using BLL.Mapping.Interfaces;
using BLL.Services;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class SelectedEquipmentMapper : ISelectedEquipmentMapper
    {
        IUnitOfWork uow;
        public SelectedEquipmentMapper(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public DalSelectedEquipment MapToDal(BllSelectedEquipment entity)
        {
            DalSelectedEquipment dalEntity = new DalSelectedEquipment
            {
                Id = entity.Id,
                Equipment_id = entity.Equipment.Id,
            };

            return dalEntity;
        }

        public BllSelectedEquipment MapToBll(DalSelectedEquipment entity)
        {
            EquipmentService equipmentService = new EquipmentService(uow);
            var bllEquipment = entity.Equipment_id != null ? equipmentService.Get((int)entity.Equipment_id) : null;

            BllSelectedEquipment bllEntity = new BllSelectedEquipment
            {
                Id = entity.Id,
                Equipment = bllEquipment
            };

            return bllEntity;
        }
    }
}
