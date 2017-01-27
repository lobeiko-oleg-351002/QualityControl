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
    public class ControlMethodsLibService : Service<BllControlMethodsLib, DalControlMethodsLib>, IControlMethodsLibService
    {
        private readonly IUnitOfWork uow;

        public ControlMethodsLibService(IUnitOfWork uow) : base(uow, uow.ControlMethodsLibs)
        {
            this.uow = uow;
        }

        public new BllControlMethodsLib Create(BllControlMethodsLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ControlMethodsLib, DalControlMethodsLib>();
                cfg.CreateMap<BllControlMethodsLib, DalControlMethodsLib>();
                cfg.CreateMap<DalControlMethodsLib, BllControlMethodsLib>();
            });
            var ormEntity = uow.ControlMethodsLibs.Create(Mapper.Map<DalControlMethodsLib>(entity));
            uow.Commit();
            entity.Id = ormEntity.id;
            ControlService controlService = new ControlService(uow);
            foreach (var Control in entity.Control)
            {
                var control = controlService.Create(Control);
                var dalControl = ControlService.MapBllToDal(control);                
                dalControl.ControlMethodsLib_id = ormEntity.id;
                var ormControl = uow.Controls.Create(dalControl);
                uow.Commit();
                Control.Id = ormControl.id;
                Control.ProtocolNumber = ormControl.protocolNumber;
               // bllEntity.Control.Add(ControlService.MapDalToBll(dalControl));
            }

            return entity;           
        }

        public override BllControlMethodsLib Get(int id)
        {
            ControlService controlService = new ControlService(uow);
            var retElement = MapDalToBll(uow.ControlMethodsLibs.Get(id));

            return retElement;
        }

        public override void Update(BllControlMethodsLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ControlMethodsLib, DalControlMethodsLib>();
                cfg.CreateMap<BllControlMethodsLib, DalControlMethodsLib>();
                cfg.CreateMap<DalControlMethodsLib, BllControlMethodsLib>();
            });
            foreach (var Control in entity.Control)
            {
                var currentControl = Control;
                if (Control.Id == 0)
                {
                    ControlService service = new ControlService(uow);
                    currentControl = service.Create(Control);
                    var dal = ControlService.MapBllToDal(currentControl);
                    dal.ControlMethodsLib_id = entity.Id;
                    uow.Controls.Create(dal);

                }
                else
                {
                    var dalControl = ControlService.MapBllToDal(currentControl);
                    dalControl.ControlMethodsLib_id = entity.Id;

                    ImageLibService imageLibService = new ImageLibService(uow);
                    imageLibService.Update(Control.ImageLib);
                    EquipmentLibService equipmentLibService = new EquipmentLibService(uow);
                    equipmentLibService.Update(Control.EquipmentLib);
                    ResultLibService resultLibService = new ResultLibService(uow);
                    resultLibService.Update(Control.ResultLib);
                    uow.Controls.Update(dalControl);
                }
                
            }
            //var ControlWithLibId = uow.Controls.GetControlsByLibId(entity.Id);
            uow.Commit();
        }

        private BllControlMethodsLib MapDalToBll(DalControlMethodsLib dalEntity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DalControlMethodsLib, BllControlMethodsLib>().ForMember(x => x.Control, opt => opt.Ignore());
            });
            BllControlMethodsLib bllEntity = Mapper.Map<BllControlMethodsLib>(dalEntity);
            ControlService controlService = new ControlService(uow);
            foreach (var Control in uow.Controls.GetControlsByLibId(dalEntity.Id))
            {
                bllEntity.Control.Add(controlService.Get(Control.Id));
            }
            return bllEntity;
        }
    }
}
