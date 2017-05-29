using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
using BLL.Mapping.Interfaces;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ControlNameLibService : Service<BllControlNameLib, DalControlNameLib>, IControlNameLibService
    {
        private readonly IUnitOfWork uow;

        public ControlNameLibService(IUnitOfWork uow) : base(uow, uow.ControlNameLibs)
        {
            this.uow = uow;
            bllMapper = new ControlNameLibMapper(uow);
        }

        ControlNameLibMapper bllMapper;

        public new BllControlNameLib Create(BllControlNameLib entity)
        {
            var ormEntity = uow.ControlNameLibs.Create(bllMapper.MapToDal(entity));
            uow.Commit();
            entity.Id = ormEntity.id;
            ISelectedControlNameMapper selectedControlNameMapper = new SelectedControlNameMapper(uow);
            foreach (var ControlName in entity.SelectedControlName)
            {
                var dalControlName = selectedControlNameMapper.MapToDal(ControlName);
                dalControlName.ControlNameLib_id = entity.Id;
                var ormControlName = uow.SelectedControlNames.Create(dalControlName);
                uow.Commit();
                ControlName.Id = ormControlName.id;
            }

            return entity;
        }

        public override BllControlNameLib Get(int id)
        {
            return bllMapper.MapToBll(uow.ControlNameLibs.Get(id));
        }

        public override void Update(BllControlNameLib entity)
        {
            ISelectedControlNameMapper selectedControlNameMapper = new SelectedControlNameMapper(uow);
            foreach (var ControlName in entity.SelectedControlName)
            {
                if (ControlName.Id == 0)
                {
                    var dalControlName = selectedControlNameMapper.MapToDal(ControlName);
                    dalControlName.ControlNameLib_id = entity.Id;
                    uow.SelectedControlNames.Create(dalControlName);
                }
            }
            var ControlNamesWithLibId = uow.SelectedControlNames.GetControlNamesByLibId(entity.Id);
            foreach (var ControlName in ControlNamesWithLibId)
            {
                bool isTrashControlName = true;
                foreach (var selectedControlName in entity.SelectedControlName)
                {
                    if (ControlName.Id == selectedControlName.Id)
                    {
                        isTrashControlName = false;
                        break;
                    }
                }
                if (isTrashControlName == true)
                {
                    uow.SelectedControlNames.Delete(ControlName);
                }
            }
            uow.Commit();
        }


    }
}
