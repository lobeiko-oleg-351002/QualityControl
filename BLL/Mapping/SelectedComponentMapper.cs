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
    public class SelectedComponentMapper : ISelectedComponentMapper
    {
        IUnitOfWork uow;
        public SelectedComponentMapper(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public DalSelectedComponent MapToDal(BllSelectedComponent entity)
        {
            DalSelectedComponent dalEntity = new DalSelectedComponent
            {
                Id = entity.Id,
                Component_id = entity.Component.Id,
            };

            return dalEntity;
        }

        public BllSelectedComponent MapToBll(DalSelectedComponent entity)
        {
            ComponentService componentService = new ComponentService(uow);
            var bllComponent = entity.Component_id != null ? componentService.Get((int)entity.Component_id) : null;

            BllSelectedComponent bllEntity = new BllSelectedComponent
            {
                Id = entity.Id,
                Component = bllComponent
            };

            return bllEntity;
        }
    }
}
