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
    public class ControlNameMapper : IControlNameMapper
    {
        IUnitOfWork uow;
        public ControlNameMapper(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public DalControlName MapToDal(BllControlName entity)
        {
            DalControlName dalEntity = new DalControlName
            {
                Id = entity.Id,
                Name = entity.Name
            };

            return dalEntity;
        }

        public BllControlName MapToBll(DalControlName entity)
        {
            if (entity != null)
            {
                BllControlName bllEntity = new BllControlName
                {
                    Id = entity.Id,
                    Name = entity.Name
                };

                return bllEntity;
            }
            return null;
        }
    }
}
