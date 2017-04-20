using BLL.Entities;
using BLL.Mapping.Interfaces;
using BLL.Services;
using BLL.Services.Interface;
using DAL;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class JournalMapper : IJournalMapper
    {
        IUnitOfWork uow;

        IComponentService componentService;
        IControlMethodsLibService controlMethodsLibService;
        ICustomerService customerService;
        IIndustrialObjectService industrialObjectService;
        IMaterialService materialService;
        IUserService userService;
        IWeldJointService weldJointService;

        public JournalMapper(IUnitOfWork uow)
        {
            this.uow = uow;
            componentService = new ComponentService(uow);
            controlMethodsLibService = new ControlMethodsLibService(uow);
            customerService = new CustomerService(uow);
            industrialObjectService = new IndustrialObjectService(uow);
            materialService = new MaterialService(uow);
            userService = new UserService(uow);
            weldJointService = new WeldJointService(uow);
        }

        public DalJournal MapToDal(BllJournal entity)
        {
            DalJournal dalEntity = new DalJournal
            {
                Id = entity.Id,
                Amount = entity.Id,
                Contract = entity.Contract,
                ControlDate = entity.ControlDate,
                Description = entity.Description,
                ModifiedDate = entity.ModifiedDate,
                RequestDate = entity.RequestDate,
                RequestNumber = entity.RequestNumber,
                Size = entity.Size,
                WeldingType = entity.WeldingType,
                UserModifierLogin = entity.UserModifierLogin,
                Component_id = entity.Component != null ? entity.Component.Id : (int?)null,
                ControlMethodsLib_id = entity.ControlMethodsLib != null ? entity.ControlMethodsLib.Id : (int?)null,
                Customer_id = entity.Customer != null ? entity.Customer.Id : (int?)null,
                IndustrialObject_id = entity.IndustrialObject != null ? entity.IndustrialObject.Id : (int?)null,
                Material_id = entity.Material != null ? entity.Material.Id : (int?)null,
                UserOwner_id = entity.UserOwner != null ? entity.UserOwner.Id : (int?)null,
                WeldJoint_id = entity.WeldJoint != null ? entity.WeldJoint.Id : (int?)null,
            };

            return dalEntity;
        }


        public BllJournal MapToBll(DalJournal entity)
        {
            BllJournal bllEntity = new BllJournal
            {
                Id = entity.Id,
                Amount = entity.Id,
                Contract = entity.Contract,
                ControlDate = entity.ControlDate,
                Description = entity.Description,
                ModifiedDate = entity.ModifiedDate,
                RequestDate = entity.RequestDate,
                RequestNumber = entity.RequestNumber,
                Size = entity.Size,
                WeldingType = entity.WeldingType,
                UserModifierLogin = entity.UserModifierLogin,
                Component = entity.Component_id != null ? componentService.Get((int)entity.Component_id) : null,
                ControlMethodsLib = entity.ControlMethodsLib_id != null ? controlMethodsLibService.Get((int)entity.ControlMethodsLib_id) : null,
                Customer = entity.Customer_id != null ? customerService.Get((int)entity.Customer_id) : null,
                IndustrialObject = entity.IndustrialObject_id != null ? industrialObjectService.Get((int)entity.IndustrialObject_id) : null,
                Material = entity.Material_id != null ? materialService.Get((int)entity.Material_id) : null,
                UserOwner = entity.UserOwner_id != null ? userService.Get((int)entity.UserOwner_id) : null,
                WeldJoint = entity.WeldJoint_id != null ? weldJointService.Get((int)entity.WeldJoint_id) : null,
            };

            return bllEntity;
        }
    }
}
