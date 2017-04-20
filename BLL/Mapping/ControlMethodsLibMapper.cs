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
    public class ControlMethodsLibMapper : IControlMethodsLibMapper
    {
        IUnitOfWork uow;
        public ControlMethodsLibMapper(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public DalControlMethodsLib MapToDal(BllControlMethodsLib entity)
        {
            return new DalControlMethodsLib
            {
                Id = entity.Id
            };
        }

        public BllControlMethodsLib MapToBll(DalControlMethodsLib entity)
        {
            BllControlMethodsLib bllEntity = new BllControlMethodsLib
            {
                Id = entity.Id
            };

            ControlService controlService = new ControlService(uow);
            foreach (var Control in uow.Controls.GetControlsByLibId(entity.Id))
            {
                bllEntity.Control.Add(controlService.Get(Control.Id));
            }
            return bllEntity;
        }
    }
}
