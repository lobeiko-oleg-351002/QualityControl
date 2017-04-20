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
    public class EquipmentLibMapper : IEquipmentLibMapper
    {
        IUnitOfWork uow;
        public EquipmentLibMapper(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public DalEquipmentLib MapToDal(BllEquipmentLib entity)
        {
            return new DalEquipmentLib
            {
                Id = entity.Id
            };
        }

        public BllEquipmentLib MapToBll(DalEquipmentLib entity)
        {
            BllEquipmentLib bllEntity = new BllEquipmentLib
            {
                Id = entity.Id
            };

            ISelectedEquipmentMapper selectedEquipmentMapper = new SelectedEquipmentMapper(uow);

            foreach (var Equipment in uow.SelectedEquipments.GetEquipmentsByLibId(bllEntity.Id))
            {
                var bllSelectedEquipment = selectedEquipmentMapper.MapToBll(Equipment);
                bllEntity.SelectedEquipment.Add(bllSelectedEquipment);
            }
            return bllEntity;
        }
    }
}
