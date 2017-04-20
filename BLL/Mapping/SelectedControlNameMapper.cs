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
    public class SelectedControlNameMapper : ISelectedControlNameMapper
    {
        IUnitOfWork uow;
        public SelectedControlNameMapper(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public DalSelectedControlName MapToDal(BllSelectedControlName entity)
        {
            DalSelectedControlName dalEntity = new DalSelectedControlName
            {
                Id = entity.Id,
                ControlName_id = entity.ControlName.Id,
            };

            return dalEntity;
        }

        public BllSelectedControlName MapToBll(DalSelectedControlName entity)
        {
            ControlNameService controlNameService = new ControlNameService(uow);
            var bllControlName = entity.ControlName_id != null ? controlNameService.Get((int)entity.ControlName_id) : null;

            BllSelectedControlName bllEntity = new BllSelectedControlName
            {
                Id = entity.Id,
                ControlName = bllControlName
            };

            return bllEntity;
        }
    }
}
