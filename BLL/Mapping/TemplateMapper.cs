using BLL.Entities;
using BLL.Mapping.Interfaces;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class TemplateMapper : ITemplateMapper
    {
        IUnitOfWork uow;
        public TemplateMapper(IUnitOfWork uow)
        {
            this.uow = uow;
            imageLibService = new ImageLibService(uow);
            weldJointService = new WeldJointService(uow);
            equipmentLibService = new EquipmentLibService(uow);
            controlNameLibService = new ControlNameLibService(uow);
            materialService = new MaterialService(uow);
            requirementDocumentationLibService = new RequirementDocumentationLibService(uow);
        }

        public DalTemplate MapToDal(BllTemplate entity)
        {
            DalTemplate dalEntity = new DalTemplate
            {
                Id = entity.Id,
                Description = entity.Description,
                Name = entity.Name,
                ImageLib_id = entity.ImageLib != null ? entity.ImageLib.Id : (int?)null,
                WeldJoint_id = entity.WeldJoint != null ? entity.WeldJoint.Id : (int?)null,
                EquipmentLib_id = entity.EquipmentLib != null ? entity.EquipmentLib.Id : (int?)null,
                ControlNameLib_id = entity.ControlNameLib != null ? entity.ControlNameLib.Id : (int?)null,
                Material_id = entity.Material != null ? entity.Material.Id : (int?)null,
                RequirementDocumentationLib_id = entity.RequirementDocumentationLib != null ? entity.RequirementDocumentationLib.Id : (int?)null,

            };

            return dalEntity;
        }

        IImageLibService imageLibService;
        IWeldJointService weldJointService;
        IEquipmentLibService equipmentLibService;
        IControlNameLibService controlNameLibService;
        IMaterialService materialService;
        IRequirementDocumentationLibService requirementDocumentationLibService;

        public BllTemplate MapToBll(DalTemplate entity)
        {
            BllTemplate bllEntity = new BllTemplate
            {
                Id = entity.Id,
                Description = entity.Description,
                Name = entity.Name,
                ImageLib = entity.ImageLib_id != null ? imageLibService.Get((int)entity.ImageLib_id) : null,
                WeldJoint = entity.WeldJoint_id != null ? weldJointService.Get((int)entity.WeldJoint_id) : null,
                EquipmentLib = entity.EquipmentLib_id != null ? equipmentLibService.Get((int)entity.EquipmentLib_id) : null,
                ControlNameLib = entity.ControlNameLib_id != null ? controlNameLibService.Get((int)entity.ControlNameLib_id) : null,
                Material = entity.Material_id != null ? materialService.Get((int)entity.Material_id) : null,
                RequirementDocumentationLib = entity.RequirementDocumentationLib_id != null ? requirementDocumentationLibService.Get((int)entity.RequirementDocumentationLib_id) : null

            };

            return bllEntity;
        }
    }
}
