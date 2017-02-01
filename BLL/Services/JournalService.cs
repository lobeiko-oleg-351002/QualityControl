using AutoMapper;
using BLL.Entities;
using BLL.Services.Interface;
using DAL;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class JournalService : Service<BllJournal, DalJournal>, IJournalService
    {
        private readonly IUnitOfWork uow;


        ComponentService ComponentService;
        CustomerService customerService;
        IndustrialObjectService industrialObjectService;
        MaterialService materialService;
        WeldJointService weldJointService;
        ControlMethodsLibService controlMethodsLibService;


        public JournalService(IUnitOfWork uow) : base(uow, uow.Journals)
        {
            this.uow = uow;
            ComponentService = new ComponentService(uow);
            customerService = new CustomerService(uow);
            industrialObjectService = new IndustrialObjectService(uow);
            materialService = new MaterialService(uow);
            weldJointService = new WeldJointService(uow);
            controlMethodsLibService = new ControlMethodsLibService(uow);
        }


        public new BllJournal Create(BllJournal entity)
        {
            ControlMethodsLibService controlMethodsLibService = new ControlMethodsLibService(uow);
            entity.ControlMethodsLib = controlMethodsLibService.Create(entity.ControlMethodsLib);
            DalJournal dalEntity = MapBllToDal(entity);
            Journal ormEntity = uow.Journals.Create(dalEntity);
            uow.Commit();
            entity.Id = ormEntity.id;
            return entity;
        }

        public override void Delete(BllJournal entity)
        {
            uow.Journals.Delete(MapBllToDal(entity));
            uow.Commit();
        }

        public new BllJournal Update(BllJournal entity)
        {
            ControlMethodsLibService controlMethodsLibService = new ControlMethodsLibService(uow);
            entity.ControlMethodsLib = controlMethodsLibService.Update(entity.ControlMethodsLib);
            uow.Journals.Update(MapBllToDal(entity));
            uow.Commit();

            return entity;
        }

        public override BllJournal Get(int id)
        {
            DalJournal dalEntity = uow.Journals.Get(id);
            return MapDalToBll(dalEntity);
        }

        public override IEnumerable<BllJournal> GetAll()
        {
            var elements = uow.Journals.GetAll();
            var retElemets = new List<BllJournal>();
            foreach (var element in elements)
            {
                retElemets.Add(MapDalToBll(element));
            }
            return retElemets;
        }

        private DalJournal MapBllToDal(BllJournal entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllJournal, DalJournal>();
            });

            DalJournal dalEntity = Mapper.Map<DalJournal>(entity);
            dalEntity.ControlMethodsLib_id = entity.ControlMethodsLib != null ? entity.ControlMethodsLib.Id : (int?)null;
            dalEntity.Component_id = entity.Component != null ? entity.Component.Id : (int?)null;
            dalEntity.Customer_id = entity.Customer != null ? entity.Customer.Id : (int?)null;
            dalEntity.Industrial_object_id = entity.IndustrialObject != null ? entity.IndustrialObject.Id : (int?)null;
            dalEntity.Material_id = entity.Material != null ? entity.Material.Id : (int?)null;
            dalEntity.Weld_joint_id = entity.WeldJoint != null ? entity.WeldJoint.Id : (int?)null;
            return dalEntity;
        }

        private BllJournal MapDalToBll(DalJournal entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DalJournal, BllJournal>();
            });

            BllJournal bllJournal = Mapper.Map<BllJournal>(entity);
            bllJournal.Component = entity.Component_id != null ? ComponentService.Get((int)entity.Component_id) : null;
            bllJournal.Customer = entity.Customer_id != null ? customerService.Get((int)entity.Customer_id) : null;
            bllJournal.IndustrialObject = entity.Industrial_object_id != null ? industrialObjectService.Get((int)entity.Industrial_object_id) : null;
            bllJournal.Material = entity.Material_id != null ? materialService.Get((int)entity.Material_id) : null;
            bllJournal.WeldJoint = entity.Weld_joint_id != null ? weldJointService.Get((int)entity.Weld_joint_id) : null;
            bllJournal.ControlMethodsLib = entity.ControlMethodsLib_id != null ? controlMethodsLibService.Get((int)entity.ControlMethodsLib_id) : null;
            return bllJournal;
        }
    }
}
