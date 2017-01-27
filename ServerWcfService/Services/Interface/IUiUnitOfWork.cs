using BLL.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerWcfService.Services.Interface
{
    public interface IUiUnitOfWork
    {
        ICertificateService Certificates { get; }
        IControlMethodDocumentationService ControlMethodDocumentations { get; }
        ICustomerService Customers { get; }
        IEquipmentService Equipments { get; }
        IMaterialService Materials { get; }
        IRequirementDocumentationService RequirementDocumentations { get; }
        IWeldJointService WeldJoints { get; }
        ITemplateService Templates { get; }
        IControlNameService ControlNames { get; }
        IComponentService Components { get; }
        IEmployeeService Employees { get; }
        IIndustrialObjectService IndustrialObjects { get; }
        IJournalService Journals { get; }
        IControlService Controls { get; }
        IImageLibService ImageLibs { get; }
        IImageService Images { get; }
        IResultService Results { get; }
        IUserService Users { get; }
        IRoleService Roles { get; }
    }
}
