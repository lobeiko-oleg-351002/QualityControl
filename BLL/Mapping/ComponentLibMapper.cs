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
    public class ComponentLibMapper : IComponentLibMapper
    {
        IUnitOfWork uow;
        public ComponentLibMapper(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public DalComponentLib MapToDal(BllComponentLib entity)
        {
            return new DalComponentLib
            {
                Id = entity.Id
            };
        }

        public BllComponentLib MapToBll(DalComponentLib entity)
        {
            BllComponentLib bllEntity = new BllComponentLib
            {
                Id = entity.Id,
            };

            ISelectedComponentMapper selectedComponentMapper = new SelectedComponentMapper(uow);

            foreach (var component in uow.SelectedComponents.GetComponentsByLibId(bllEntity.Id))
            {
                var bllSelectedComponent = selectedComponentMapper.MapToBll(component);
                bllEntity.SelectedComponent.Add(bllSelectedComponent);
            }
            return bllEntity;
        }
    }
}
