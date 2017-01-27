using BLL.Services;
using BLL.Services.Interface;
using DAL.Repositories;
using ORM;
using ServerWcfService.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServerWcfService.Services
{

    public class UiUnitOfWork : IUiUnitOfWork
    {
        private static UiUnitOfWork instance;

        private static UnitOfWork uow { get; set; }

        public static UiUnitOfWork Instance
        {
            get { return instance ?? (instance = new UiUnitOfWork()); }
        }

        protected UiUnitOfWork() { }

        public void Init(ServiceDB context)
        {
            uow = new UnitOfWork(context);
        }

        private CertificateService certificateService;

        public ICertificateService Certificates
            => certificateService ?? (certificateService = new CertificateService(uow));

        private ControlMethodDocumentationService controlMethodDocumentationService;

        public IControlMethodDocumentationService ControlMethodDocumentations
            => controlMethodDocumentationService ?? (controlMethodDocumentationService = new ControlMethodDocumentationService(uow));

        private CustomerService customerService;

        public ICustomerService Customers
            => customerService ?? (customerService = new CustomerService(uow));

        private EquipmentService equipmentService;

        public IEquipmentService Equipments
            => equipmentService ?? (equipmentService = new EquipmentService(uow));

        private MaterialService materialService;

        public IMaterialService Materials
            => materialService ?? (materialService = new MaterialService(uow));

        private RequirementDocumentationService requirementDocumentationService;

        public IRequirementDocumentationService RequirementDocumentations
            => requirementDocumentationService ?? (requirementDocumentationService = new RequirementDocumentationService(uow));

        private WeldJointService weldJointService;

        public IWeldJointService WeldJoints
            => weldJointService ?? (weldJointService = new WeldJointService(uow));

        private TemplateService templateService;

        public ITemplateService Templates
            => templateService ?? (templateService = new TemplateService(uow));

        private ControlNameService controlNameService;

        public IControlNameService ControlNames
            => controlNameService ?? (controlNameService = new ControlNameService(uow));

        private ComponentService componentService;

        public IComponentService Components
            => componentService ?? (componentService = new ComponentService(uow));

        private EmployeeService EmployeeService;

        public IEmployeeService Employees
            => EmployeeService ?? (EmployeeService = new EmployeeService(uow));

        private IndustrialObjectService IndustrialObjectService;

        public IIndustrialObjectService IndustrialObjects
            => IndustrialObjectService ?? (IndustrialObjectService = new IndustrialObjectService(uow));

        private JournalService JournalService;

        public IJournalService Journals
            => JournalService ?? (JournalService = new JournalService(uow));

        private ControlService ControlService;

        public IControlService Controls
            => ControlService ?? (ControlService = new ControlService(uow));

        private ImageLibService ImageLibService;

        public IImageLibService ImageLibs
            => ImageLibService ?? (ImageLibService = new ImageLibService(uow));

        private ImageService ImageService;

        public IImageService Images
            => ImageService ?? (ImageService = new ImageService(uow));

        private ResultService ResultService;

        public IResultService Results
            => ResultService ?? (ResultService = new ResultService(uow));

        private UserService UserService;

        public IUserService Users
            => UserService ?? (UserService = new UserService(uow));

        private RoleService RoleService;

        public IRoleService Roles
            => RoleService ?? (RoleService = new RoleService(uow));
    }

    
}
