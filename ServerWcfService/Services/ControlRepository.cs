using AutoMapper;
using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using ServerWcfService.Services;
using ServerWcfService.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities;
using UIL.Entities.Interface;

namespace ServerWcfService
{
    public class ControlRepository : Repository<UilControl, BllControl>, IControlRepository
    {
        private readonly IControlService ControlService;

        public ControlRepository() : base(UiUnitOfWork.Instance.Controls)
        {
            ControlService = UiUnitOfWork.Instance.Controls;
        }

        public override void Create(UilControl entity)
        { 
            ControlService.Create(MapUilToBll(entity));
        }

        public override void Update(UilControl entity)
        {
            ControlService.Update(MapUilToBll(entity));
        }

        public override void Delete(UilControl entity)
        {
            ControlService.Delete(MapUilToBll(entity));
        }

        public override IEnumerable<UilControl> GetAll()
        {
            var bllEntities = ControlService.GetAll();
            var retElements = new List<UilControl>();
            foreach (var element in bllEntities)
            {
                retElements.Add(MapBllToUil(element));
            }
            return retElements;
        }

        public static UilControl MapBllToUil(BllControl bllEntity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllControlMethodDocumentationLib, UilControlMethodDocumentationLib>();
                cfg.CreateMap<BllControlMethodDocumentation, UilControlMethodDocumentation>();
                cfg.CreateMap<BllSelectedControlMethodDocumentation, UilSelectedControlMethodDocumentation>();
                cfg.CreateMap<BllRequirementDocumentationLib, UilRequirementDocumentationLib>();
                cfg.CreateMap<BllRequirementDocumentation, UilRequirementDocumentation>();
                cfg.CreateMap<BllSelectedRequirementDocumentation, UilSelectedRequirementDocumentation>();
                cfg.CreateMap<BllEmployeeLib, UilEmployeeLib>();
                cfg.CreateMap<BllEmployee, UilEmployee>();
                cfg.CreateMap<BllSelectedEmployee, UilSelectedEmployee>();
                cfg.CreateMap<BllEquipment, UilEquipment>();
                cfg.CreateMap<BllEquipmentLib, UilEquipmentLib>();
                cfg.CreateMap<BllSelectedEquipment, UilSelectedEquipment>();
                cfg.CreateMap<BllControlName, UilControlName>();
                cfg.CreateMap<BllImageLib, UilImageLib>().ForMember(x => x.Image, opt => opt.Ignore());
                cfg.CreateMap<BllControl, UilControl>().ForMember(x => x.ImageLib, opt => opt.Ignore());
                                                       //.ForMember(x => x.ControlMethodDocumentationLib, opt => opt.Ignore());
                cfg.CreateMap<BllImage, UilImage>();
                cfg.CreateMap<BllResult, UilResult>();
                cfg.CreateMap<BllResultLib, UilResultLib>();
                cfg.CreateMap<BllSelectedCertificate, UilSelectedCertificate>();
            });
            UilControl uilEntity = Mapper.Map<UilControl>(bllEntity);
            uilEntity.ControlMethodDocumentationLib = Mapper.Map<UilControlMethodDocumentationLib>(bllEntity.ControlMethodDocumentationLib);
            uilEntity.ControlName = Mapper.Map<UilControlName>(bllEntity.ControlName);
            uilEntity.EquipmentLib = Mapper.Map<UilEquipmentLib>(bllEntity.EquipmentLib);
            //if (uilEntity.EquipmentLib != null)
            //{
            //    foreach (var bllEquipment in bllEntity.EquipmentLib.SelectedEquipment)
            //    {
            //        uilEntity.EquipmentLib.SelectedEquipment.Add(Mapper.Map<UilSelectedEquipment>(bllEquipment));
            //    }
           // }
            uilEntity.ImageLib = Mapper.Map<UilImageLib>(bllEntity.ImageLib);
            if (uilEntity.ImageLib != null)
            {
                foreach (var bllImage in bllEntity.ImageLib.Image)
                {
                    uilEntity.ImageLib.Image.Add(Mapper.Map<UilImage>(bllImage));
                }
            }
            uilEntity.EmployeeLib = Mapper.Map<UilEmployeeLib>(bllEntity.EmployeeLib);
            uilEntity.RequirementDocumentationLib = Mapper.Map<UilRequirementDocumentationLib>(bllEntity.RequirementDocumentationLib);
            uilEntity.ResultLib = Mapper.Map<UilResultLib>(bllEntity.ResultLib);
            //Sif (uilEntity.ResultLib != null)
            //{
            //    foreach (var bllResult in bllEntity.ResultLib.Result)
            //    {
            //        uilEntity.ResultLib.Result.Add(Mapper.Map<UilResult>(bllResult));
            //    }
            //}

            return uilEntity;
        }

        public static BllControl MapUilToBll(UilControl entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UilControlMethodDocumentationLib, BllControlMethodDocumentationLib>();
                cfg.CreateMap<UilSelectedControlMethodDocumentation, BllSelectedControlMethodDocumentation>();
                cfg.CreateMap<UilControlMethodDocumentation, BllControlMethodDocumentation>();
                cfg.CreateMap<UilRequirementDocumentationLib, BllRequirementDocumentationLib>();
                cfg.CreateMap<UilSelectedRequirementDocumentation, BllSelectedRequirementDocumentation>();
                cfg.CreateMap<UilRequirementDocumentation, BllRequirementDocumentation>();
                cfg.CreateMap<UilEmployeeLib, BllEmployeeLib>();
                cfg.CreateMap<UilSelectedEmployee, BllSelectedEmployee>();
                cfg.CreateMap<UilEmployee, BllEmployee>();
                cfg.CreateMap<UilEquipment, BllEquipment>();
                cfg.CreateMap<UilEquipmentLib, BllEquipmentLib>();
                cfg.CreateMap<UilSelectedEquipment, BllSelectedEquipment>();
                cfg.CreateMap<UilControlName, BllControlName>();
                cfg.CreateMap<UilImageLib, BllImageLib>().ForMember(x => x.Image, opt => opt.Ignore());
                cfg.CreateMap<UilControl, BllControl>();
                cfg.CreateMap<UilImage, BllImage>();
                cfg.CreateMap<UilResult, BllResult>();
                cfg.CreateMap<UilResultLib, BllResultLib>();
                cfg.CreateMap<UilCertificate, BllCertificate>();
                cfg.CreateMap<UilCertificateLib, BllCertificateLib>();
                cfg.CreateMap<UilSelectedCertificate, BllSelectedCertificate>();
            });

            BllControl bllEntity = Mapper.Map<BllControl>(entity);
            bllEntity.ImageLib = Mapper.Map<BllImageLib>(entity.ImageLib);
            if (bllEntity.ImageLib != null)
            {
                foreach (var uilImage in entity.ImageLib.Image)
                {
                    bllEntity.ImageLib.Image.Add(Mapper.Map<BllImage>(uilImage));
                }
            }
            return bllEntity;
        }
    }
}
