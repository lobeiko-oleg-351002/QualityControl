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
    public class EquipmentLibService : Service<BllEquipmentLib, DalEquipmentLib>, IEquipmentLibService
    {
        private readonly IUnitOfWork uow;
        EquipmentLibMapper bllMapper;
        public EquipmentLibService(IUnitOfWork uow) : base(uow, uow.EquipmentLibs)
        {
            this.uow = uow;
            bllMapper = new EquipmentLibMapper(uow);
        }

        public new BllEquipmentLib Create(BllEquipmentLib entity)
        {
            var ormEntity = uow.EquipmentLibs.Create(bllMapper.MapToDal(entity));
            uow.Commit();
            entity.Id = ormEntity.id;
            ISelectedEquipmentMapper selectedEquipmentMapper = new SelectedEquipmentMapper(uow);
            foreach (var Equipment in entity.SelectedEquipment)
            {
                var dalEquipment = selectedEquipmentMapper.MapToDal(Equipment);
                dalEquipment.EquipmentLib_id = entity.Id;
                var ormEquipment = uow.SelectedEquipments.Create(dalEquipment);
                uow.Commit();
                Equipment.Id = ormEquipment.id;
            }

            return entity;
        }

        public override BllEquipmentLib Get(int id)
        {
            return bllMapper.MapToBll(uow.EquipmentLibs.Get(id));
        }

        public new BllEquipmentLib Update(BllEquipmentLib entity)
        {
            ISelectedEquipmentMapper selectedEquipmentMapper = new SelectedEquipmentMapper(uow);
            foreach (var Equipment in entity.SelectedEquipment)
            {
                if (Equipment.Id == 0)
                {
                    var dalEquipment = selectedEquipmentMapper.MapToDal(Equipment);
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

        //private BllEquipmentLib MapDalToBll(DalEquipmentLib dalEntity)
        //{
        //    Mapper.Initialize(cfg =>
        //    {
        //        cfg.CreateMap<DalEquipmentLib, BllEquipmentLib>();
        //        cfg.CreateMap<DalEquipment, BllEquipment>();
        //        cfg.CreateMap<DalSelectedEquipment, BllSelectedEquipment>();
        //    });
        //    BllEquipmentLib bllEntity = Mapper.Map<BllEquipmentLib>(dalEntity);

        //    SelectedEquipmentService selectedEquipmentService = new SelectedEquipmentService(uow);
        //    EquipmentService EquipmentService = new EquipmentService(uow);
        //    foreach (var Equipment in uow.SelectedEquipments.GetEquipmentsByLibId(dalEntity.Id))
        //    {
        //        var bllEquipment = Equipment.Equipment_id != null ? EquipmentService.Get((int)Equipment.Equipment_id) : null;
        //        var bllSelectedEquipment = Mapper.Map<BllSelectedEquipment>(Equipment);
        //        bllSelectedEquipment.Equipment = bllEquipment;
        //        bllEntity.SelectedEquipment.Add(bllSelectedEquipment);

        //    }
        //    return bllEntity;
        //}
    }
}