using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IUnitOfWork
    {   
        IRoleRepository Roles { get; }
        IUserRepository Users { get; }
        ICertificateRepository Certificates { get; }
        IComponentRepository Components { get; }
        ICustomerRepository Customers { get; }
        IEmployeeRepository Employees { get; }
        IEquipmentRepository Equipments { get; }
        IIndustrialObjectRepository IndustrialObjects { get; }
        IMaterialRepository Materials { get; }
        ITemplateRepository Templates { get; }
        IControlMethodDocumentationRepository ControlMethodDocumentations { get; }
        IRequirementDocumentationRepository RequirementDocumentations { get; }
        ICertificateLibRepository CertificateLibs { get; }
        IComponentLibRepository ComponentLibs { get; }
        IControlMethodsLibRepository ControlMethodsLibs { get; }
        IControlNameLibRepository ControlNameLibs { get; }
        IControlNameRepository ControlNames { get; }
        IControlRepository Controls { get; }
        IImageLibRepository ImageLibs { get; }
        IImageRepository Images { get; }
        IJournalRepository Journals { get; }
        IResultRepository Results { get; }
        IWeldJointRepository WeldJoints { get; }
        ISelectedCertificateRepository SelectedCertificates { get; }
        ISelectedComponentRepository SelectedComponents { get; }
        ISelectedControlNameRepository SelectedControlNames { get; }
        IResultLibRepository ResultLibs { get; }
        IEquipmentLibRepository EquipmentLibs { get; }
        ISelectedEquipmentRepository SelectedEquipments { get; }
        IControlMethodDocumentationLibRepository ControlMethodDocumentationLibs { get; }
        ISelectedControlMethodDocumentationRepository SelectedControlMethodDocumentations { get; }
        IRequirementDocumentationLibRepository RequirementDocumentationLibs { get; }
        ISelectedRequirementDocumentationRepository SelectedRequirementDocumentations { get; }
        IEmployeeLibRepository EmployeeLibs { get; }
        ISelectedEmployeeRepository SelectedEmployees { get; }

        void Commit();
    }
}
