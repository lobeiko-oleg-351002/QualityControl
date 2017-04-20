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
    public class ControlNameLibMapper : IControlNameLibMapper
    {
        IUnitOfWork uow;
        public ControlNameLibMapper(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public DalControlNameLib MapToDal(BllControlNameLib entity)
        {
            return new DalControlNameLib
            {
                Id = entity.Id
            };
        }

        public BllControlNameLib MapToBll(DalControlNameLib entity)
        {
            BllControlNameLib bllEntity = new BllControlNameLib
            {
                Id = entity.Id
            };

            ISelectedControlNameMapper selectedControlNameMapper = new SelectedControlNameMapper(uow);

            foreach (var ControlName in uow.SelectedControlNames.GetControlNamesByLibId(bllEntity.Id))
            {
                var bllSelectedControlName = selectedControlNameMapper.MapToBll(ControlName);
                bllEntity.SelectedControlName.Add(bllSelectedControlName);
            }
            return bllEntity;
        }
    }
}
