using AutoMapper;
using BLL.Entities;
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
        }

        public new BllControlNameLib Create(BllControlNameLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllSelectedControlName, DalSelectedControlName>();
                cfg.CreateMap<ControlNameLib, DalControlNameLib>();
                cfg.CreateMap<BllControlNameLib, DalControlNameLib>();
                cfg.CreateMap<DalControlNameLib, BllControlNameLib>();
            });
            var ormEntity = uow.ControlNameLibs.Create(Mapper.Map<DalControlNameLib>(entity));
            uow.Commit();
            var dalEntity = Mapper.Map<DalControlNameLib>(ormEntity);
            foreach (var ControlName in entity.SelectedControlName)
            {
                Mapper.CreateMap<BllSelectedControlName, DalSelectedControlName>();
                var dalControlName = Mapper.Map<DalSelectedControlName>(ControlName);
                dalControlName.ControlNameLib_id = dalEntity.Id;
                uow.SelectedControlNames.Create(dalControlName);
            }
            uow.Commit();
            Mapper.CreateMap<DalControlNameLib, BllControlNameLib>();
            return Mapper.Map<BllControlNameLib>(dalEntity);
        }

        public override BllControlNameLib Get(int id)
        {
            Mapper.CreateMap<DalControlNameLib, BllControlNameLib>();
            return MapDalToBll(uow.ControlNameLibs.Get(id));
        }

        public override void Update(BllControlNameLib entity)
        {
            
            foreach (var ControlName in entity.SelectedControlName)
            {
                if (ControlName.Id == 0)
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<BllSelectedControlName, DalSelectedControlName>();
                        cfg.CreateMap<ControlNameLib, DalControlNameLib>();
                        cfg.CreateMap<BllControlNameLib, DalControlNameLib>();
                        cfg.CreateMap<DalControlNameLib, BllControlNameLib>();
                    });
                    var dalControlName = Mapper.Map<DalSelectedControlName>(ControlName);
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

        private BllControlNameLib MapDalToBll(DalControlNameLib dalEntity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DalControlNameLib, BllControlNameLib>();
                cfg.CreateMap<DalControlName, BllControlName>();
                cfg.CreateMap<DalSelectedControlName, BllSelectedControlName>();
            });
            BllControlNameLib bllEntity = Mapper.Map<BllControlNameLib>(dalEntity);

            SelectedControlNameService selectedControlNameService = new SelectedControlNameService(uow);
            ControlNameService ControlNameService = new ControlNameService(uow);
            foreach (var ControlName in uow.SelectedControlNames.GetControlNamesByLibId(dalEntity.Id))
            {
                var bllControlName = ControlName.ControlName_id != null ? ControlNameService.Get((int)ControlName.ControlName_id) : null;
                var bllSelectedControlName = Mapper.Map<BllSelectedControlName>(ControlName);
                bllSelectedControlName.ControlName = bllControlName;
                bllEntity.SelectedControlName.Add(bllSelectedControlName);

            }
            return bllEntity;
        }

    }
}
