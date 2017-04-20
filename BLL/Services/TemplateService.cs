using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
using BLL.Mapping.Interfaces;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TemplateService : Service<BllTemplate, DalTemplate>, ITemplateService
    {
        private readonly IUnitOfWork uow;
        ITemplateMapper bllMapper;
        public TemplateService(IUnitOfWork uow) : base(uow, uow.Templates)
        {
            this.uow = uow;
            bllMapper = new TemplateMapper(uow);
        }

        public override void Create(BllTemplate entity)
        {
            ImageLibService imageLibService = new ImageLibService(uow);
            var imageLib = imageLibService.Create(entity.ImageLib);
            entity.ImageLib = imageLib;

            EquipmentLibService equipmentLibService = new EquipmentLibService(uow);
            var equipmentLib = equipmentLibService.Create(entity.EquipmentLib);
            entity.EquipmentLib = equipmentLib;

            ControlNameLibService controlNameLibService = new ControlNameLibService(uow);
            var controlNameLib = controlNameLibService.Create(entity.ControlNameLib);           
            entity.ControlNameLib = controlNameLib;

            RequirementDocumentationLibService requirementDocumentationLibService = new RequirementDocumentationLibService(uow);
            var requirementDocumentationLib = requirementDocumentationLibService.Create(entity.RequirementDocumentationLib);
            entity.RequirementDocumentationLib = requirementDocumentationLib;

            uow.Templates.Create(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllTemplate entity)
        {
            uow.Templates.Delete(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Update(BllTemplate entity)
        {
            ImageLibService imageLibService = new ImageLibService(uow);
            imageLibService.Update(entity.ImageLib);
            EquipmentLibService EquipmentLibService = new EquipmentLibService(uow);
            EquipmentLibService.Update(entity.EquipmentLib);
            ControlNameLibService controlNameLibService = new ControlNameLibService(uow);
            controlNameLibService.Update(entity.ControlNameLib);
            RequirementDocumentationLibService requirementDocumentationLibService = new RequirementDocumentationLibService(uow);
            requirementDocumentationLibService.Update(entity.RequirementDocumentationLib);
            uow.Templates.Update(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override BllTemplate Get(int id)
        {
            DalTemplate dalEntity = uow.Templates.Get(id);
            return bllMapper.MapToBll(dalEntity);
        }

        public override IEnumerable<BllTemplate> GetAll()
        {
            var elements = uow.Templates.GetAll();
            var retElemets = new List<BllTemplate>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }
        public BllTemplate GetTemplateByName(string name)
        {
            DalTemplate dalEntity = uow.Templates.GetTemplateByName(name);
            return bllMapper.MapToBll(dalEntity);
        }

        //private BllTemplate MapDalToBll(DalTemplate dalEntity)
        //{
        //    Mapper.CreateMap<DalTemplate, BllTemplate>();
        //    //DalTemplate dalEntity = uow.Templates.Get(id);
        //    BllTemplate bllEntity = Mapper.Map<BllTemplate>(dalEntity);
        //    MaterialService materialService = new MaterialService(uow);
        //    WeldJointService weldJointService = new WeldJointService(uow);
        //    EquipmentLibService equipmentLibService = new EquipmentLibService(uow);
        //    ImageLibService imageLibService = new ImageLibService(uow);
        //    ControlNameLibService controlNameLibService = new ControlNameLibService(uow);
        //    RequirementDocumentationLibService requirementDocumentationLibService = new RequirementDocumentationLibService(uow);
        //    bllEntity.Material = dalEntity.Material_id != null ? materialService.Get((int)dalEntity.Material_id) : null;
        //    bllEntity.WeldJoint = dalEntity.WeldJoint_id != null ? weldJointService.Get((int)dalEntity.WeldJoint_id) : null;
        //    bllEntity.EquipmentLib = dalEntity.EquipmentLib_id != null ? equipmentLibService.Get((int)dalEntity.EquipmentLib_id) : null;
        //    bllEntity.ImageLib = dalEntity.ImageLib_id != null ? imageLibService.Get((int)dalEntity.ImageLib_id) : null;
        //    bllEntity.ControlNameLib = dalEntity.ControlNameLib_id != null ? controlNameLibService.Get((int)dalEntity.ControlNameLib_id) : null;
        //    bllEntity.RequirementDocumentationLib = dalEntity.RequirementDocumentationLib_id != null ? requirementDocumentationLibService.Get((int)dalEntity.RequirementDocumentationLib_id) : null;
        //    return bllEntity;
        //}

        //private DalTemplate MapBllToDal(BllTemplate entity)
        //{
        //    Mapper.CreateMap<BllTemplate, DalTemplate>();
        //    DalTemplate dalEntity = Mapper.Map<DalTemplate>(entity);
        //    dalEntity.Material_id = entity.Material != null ? entity.Material.Id : (int?)null;
        //    dalEntity.WeldJoint_id = entity.WeldJoint != null ? entity.WeldJoint.Id : (int?)null;
        //    dalEntity.EquipmentLib_id = entity.EquipmentLib != null ? entity.EquipmentLib.Id : (int?)null;
        //    dalEntity.ImageLib_id = entity.ImageLib != null ? entity.ImageLib.Id : (int?)null;
        //    dalEntity.ControlNameLib_id = entity.ControlNameLib != null ? entity.ControlNameLib.Id : (int?)null;
        //    dalEntity.RequirementDocumentationLib_id = entity.RequirementDocumentationLib != null ? entity.RequirementDocumentationLib.Id : (int?)null;
        //    return dalEntity;
        //}
    }
}
