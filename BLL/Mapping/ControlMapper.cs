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
    public class ControlMapper : IControlMapper
    {
        IUnitOfWork uow;
        public ControlMapper(IUnitOfWork uow)
        {
            this.uow = uow;
            controlNameService = new ControlNameService(uow);
            employeeLibService = new EmployeeLibService(uow);
            equipmentLibService = new EquipmentLibService(uow);
            imageLibService = new ImageLibService(uow);
            requirementDocumentationLibService = new RequirementDocumentationLibService(uow);
            resultLibService = new ResultLibService(uow);
            controlMethodDocumentationLibService = new ControlMethodDocumentationLibService(uow);
        }

        public DalControl MapToDal(BllControl entity)
        {
            DalControl dalEntity = new DalControl
            {
                Id = entity.Id,
                Additionally = entity.Additionally,              
                IsControlled = entity.IsControlled,
                Light = entity.Light,
                Number = entity.Number,
                ProtocolNumber = entity.ProtocolNumber,
                EmployeeLib_id = entity.EmployeeLib != null ? entity.EmployeeLib.Id : (int?)null,
                EquipmentLib_id = entity.EquipmentLib != null ? entity.EquipmentLib.Id : (int?)null,
                ControlMethodDocumentationLib_id = entity.ControlMethodDocumentationLib != null ? entity.ControlMethodDocumentationLib.Id : (int?)null,
                ImageLib_id = entity.ImageLib != null ? entity.ImageLib.Id : (int?)null,
                RequirementDocumentationLib_id = entity.RequirementDocumentationLib != null ? entity.RequirementDocumentationLib.Id : (int?)null,
                ResultLib_id = entity.ResultLib != null ? entity.ResultLib.Id : (int?)null,
                ControlName_id = entity.ControlName != null ? entity.ControlName.Id : (int?)null
            };

            return dalEntity;
        }

        IControlNameService controlNameService;
        IControlMethodDocumentationLibService controlMethodDocumentationLibService;
        IEmployeeLibService employeeLibService;
        IEquipmentLibService equipmentLibService;
        IImageLibService imageLibService;
        IRequirementDocumentationLibService requirementDocumentationLibService;
        IResultLibService resultLibService;

        public BllControl MapToBll(DalControl entity)
        {
            BllControl bllEntity = new BllControl
            {
                Id = entity.Id,
                ControlName = entity.ControlName_id != null ? controlNameService.Get((int)entity.ControlName_id) : null,
                EmployeeLib = entity.EmployeeLib_id != null ? employeeLibService.Get((int)entity.EmployeeLib_id) : null,
                ControlMethodDocumentationLib = entity.ControlMethodDocumentationLib_id != null ? controlMethodDocumentationLibService.Get((int)entity.ControlMethodDocumentationLib_id) : null,
                EquipmentLib = entity.EquipmentLib_id != null ? equipmentLibService.Get((int)entity.EquipmentLib_id) : null,
                ImageLib = entity.ImageLib_id != null ? imageLibService.Get((int)entity.ImageLib_id) : null,
                RequirementDocumentationLib = entity.RequirementDocumentationLib_id != null ? requirementDocumentationLibService.Get((int)entity.RequirementDocumentationLib_id) : null,
                ResultLib = entity.ResultLib_id != null ? resultLibService.Get((int)entity.ResultLib_id) : null,
                Additionally = entity.Additionally,
                IsControlled = entity.IsControlled,
                Light = entity.Light,
                Number = entity.Number,
                ProtocolNumber = entity.ProtocolNumber,
            };

            return bllEntity;
        }
    }
}
