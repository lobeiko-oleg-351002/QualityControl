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
    public class EquipmentLibService : Service<BllEquipmentLib, DalEquipmentLib>, IEquipmentLibService
    {
        private readonly IUnitOfWork uow;

        public EquipmentLibService(IUnitOfWork uow) : base(uow, uow.EquipmentLibs)
        {
            this.uow = uow;
        }

        public new BllEquipmentLib Create(BllEquipmentLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllSelectedEquipment, DalSelectedEquipment>();
                cfg.CreateMap<EquipmentLib, DalEquipmentLib>();
                cfg.CreateMap<BllEquipmentLib, DalEquipmentLib>();
                cfg.CreateMap<DalEquipmentLib, BllEquipmentLib>();
            });
            var ormEntity = uow.EquipmentLibs.Create(Mapper.Map<DalEquipmentLib>(entity));
            uow.Commit();
            entity.Id = ormEntity.id;
            foreach (var Equipment in entity.SelectedEquipment)
            {
                Mapper.CreateMap<BllSelectedEquipment, DalSelectedEquipment>();
                var dalEquipment = Mapper.Map<DalSelectedEquipment>(Equipment);
                dalEquipment.EquipmentLib_id = entity.Id;
                var ormEquipment = uow.SelectedEquipments.Create(dalEquipment);
                uow.Commit();
                Equipment.Id = ormEquipment.id;
            }

            return entity;
        }

        public override BllEquipmentLib Get(int id)
        {
            Mapper.CreateMap<DalEquipmentLib, BllEquipmentLib>();
            return MapDalToBll(uow.EquipmentLibs.Get(id));
        }

        public new BllEquipmentLib Update(BllEquipmentLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllSelectedEquipment, DalSelectedEquipment>();
                cfg.CreateMap<EquipmentLib, DalEquipmentLib>();
                cfg.CreateMap<BllEquipmentLib, DalEquipmentLib>();
                cfg.CreateMap<DalEquipmentLib, BllEquipmentLib>();
            });
            foreach (var Equipment in entity.SelectedEquipment)
            {
                if (Equipment.Id == 0)
                {
                    var dalEquipment = Mapper.Map<DalSelectedEquipment>(Equipment);
                    dalEquipment.EquipmentLib_id = entity.Id;
                    var ormEq = uow.SelectedEquipments.Create(dalEquipment);
                    uow.Commit();
                    Equipment.Id = ormEq.id;
                }
            }
            var EquipmentsWithLibId = uow.SelectedEquipments.GetEquipmentsByLibId(entity.Id);
            foreach (var Equipment in EquipmentsWithLibId)
            {
                bool isTrashEquipment = true;
                foreach (var selectedEquipment in entity.SelectedEquipment)
                {
                    if (Equipment.Id == selectedEquipment.Id)
                    {
                        isTrashEquipment = false;
                        break;
                    }
                }
                if (isTrashEquipment == true)
                {
                    uow.SelectedEquipments.Delete(Equipment);
                }
            }
            uow.Commit();

            return entity;
        }

        private BllEquipmentLib MapDalToBll(DalEquipmentLib dalEntity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DalEquipmentLib, BllEquipmentLib>();
                cfg.CreateMap<DalEquipment, BllEquipment>();
                cfg.CreateMap<DalSelectedEquipment, BllSelectedEquipment>();
            });
            BllEquipmentLib bllEntity = Mapper.Map<BllEquipmentLib>(dalEntity);

            SelectedEquipmentService selectedEquipmentService = new SelectedEquipmentService(uow);
            EquipmentService EquipmentService = new EquipmentService(uow);
            foreach (var Equipment in uow.SelectedEquipments.GetEquipmentsByLibId(dalEntity.Id))
            {
                var bllEquipment = Equipment.Equipment_id != null ? EquipmentService.Get((int)Equipment.Equipment_id) : null;
                var bllSelectedEquipment = Mapper.Map<BllSelectedEquipment>(Equipment);
                bllSelectedEquipment.Equipment = bllEquipment;
                bllEntity.SelectedEquipment.Add(bllSelectedEquipment);

            }
            return bllEntity;
        }
    }
}