using AutoMapper;
using BLL.Entities;
using BLL.Services.Interface;
using ServerWcfService.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities;

namespace ServerWcfService.Services
{
    public class TemplateRepository : Repository<UilTemplate,BllTemplate>, ITemplateRepository
    {
        private readonly ITemplateService templateService;

        public TemplateRepository() : base(UiUnitOfWork.Instance.Templates)
        {
            templateService = UiUnitOfWork.Instance.Templates;
        }

        public override void Create(UilTemplate entity)
        {          
            templateService.Create(MapUilToBll(entity));
        }

        public override void Update(UilTemplate entity)
        {
            templateService.Update(MapUilToBll(entity));
        }

        public override void Delete(UilTemplate entity)
        {
            templateService.Delete(MapUilToBll(entity));
        }

        public override IEnumerable<UilTemplate> GetAll()
        {
            var bllEntities = templateService.GetAll();
            var retElements = new List<UilTemplate>();
            foreach (var element in bllEntities)
            {
                retElements.Add(MapBllToUil(element));
            }
            return retElements;
        }

        public static UilTemplate MapBllToUil(BllTemplate bllEntity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllMaterial, UilMaterial>();
                cfg.CreateMap<BllWeldJoint, UilWeldJoint>();
                cfg.CreateMap<BllEquipmentLib, UilEquipmentLib>();
                cfg.CreateMap<BllSelectedEquipment, UilSelectedEquipment>();
                cfg.CreateMap<BllEquipment, UilEquipment>();
                cfg.CreateMap<BllControlNameLib, UilControlNameLib>();
                cfg.CreateMap<BllSelectedControlName, UilSelectedControlName>();
                cfg.CreateMap<BllControlName, UilControlName>();
                cfg.CreateMap<BllImageLib, UilImageLib>().ForMember(x => x.Image, opt => opt.Ignore());
                cfg.CreateMap<BllTemplate, UilTemplate>().ForMember(x => x.ImageLib, opt => opt.Ignore());
                cfg.CreateMap<BllImage, UilImage>();
                cfg.CreateMap<BllRequirementDocumentationLib, UilRequirementDocumentationLib>();
                cfg.CreateMap<BllSelectedRequirementDocumentation, UilSelectedRequirementDocumentation>();
                cfg.CreateMap<BllRequirementDocumentation, UilRequirementDocumentation>();
            });
            UilTemplate uilEntity = Mapper.Map<UilTemplate>(bllEntity);
            uilEntity.Material = Mapper.Map<UilMaterial>(bllEntity.Material);
            uilEntity.WeldJoint = Mapper.Map<UilWeldJoint>(bllEntity.WeldJoint);
            uilEntity.EquipmentLib = Mapper.Map<UilEquipmentLib>(bllEntity.EquipmentLib);
            uilEntity.ImageLib = Mapper.Map<UilImageLib>(bllEntity.ImageLib);
            if (uilEntity.ImageLib != null)
            {
                foreach (var bllImage in bllEntity.ImageLib.Image)
                {
                    uilEntity.ImageLib.Image.Add(Mapper.Map<UilImage>(bllImage));
                }
            }
            uilEntity.ControlNameLib = Mapper.Map<UilControlNameLib>(bllEntity.ControlNameLib);
            uilEntity.RequirementDocumentationLib = Mapper.Map<UilRequirementDocumentationLib>(bllEntity.RequirementDocumentationLib);
            return uilEntity;
        }

        public static BllTemplate MapUilToBll(UilTemplate entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UilMaterial, BllMaterial>();
                cfg.CreateMap<UilWeldJoint, BllWeldJoint>();
                cfg.CreateMap<UilEquipmentLib, BllEquipmentLib>();
                cfg.CreateMap<UilSelectedEquipment, BllSelectedEquipment>();
                cfg.CreateMap<UilEquipment, BllEquipment>();
                cfg.CreateMap<UilControlNameLib, BllControlNameLib>();
                cfg.CreateMap<UilControlName, BllControlName>();
                cfg.CreateMap<UilSelectedControlName, BllSelectedControlName>();
                cfg.CreateMap<UilImageLib, BllImageLib>();
                cfg.CreateMap<UilTemplate, BllTemplate>();
                cfg.CreateMap<UilImage, BllImage>();
                cfg.CreateMap<UilRequirementDocumentationLib, BllRequirementDocumentationLib>();
                cfg.CreateMap<UilSelectedRequirementDocumentation, BllSelectedRequirementDocumentation>();
                cfg.CreateMap<UilRequirementDocumentation, BllRequirementDocumentation>();
            });

            BllTemplate bllEntity = Mapper.Map<BllTemplate>(entity);
            //bllEntity.ControlNameLib = Mapper.Map<BllControlNameLib>(entity.ControlNameLib);
            //bllEntity.Equipment = Mapper.Map<BllEquipment>(entity.Equipment);
            //bllEntity.Material = Mapper.Map<BllMaterial>(entity.Material);
            //bllEntity.WeldJoint = Mapper.Map<BllWeldJoint>(entity.WeldJoint);

            return bllEntity;
        }
    }
}
