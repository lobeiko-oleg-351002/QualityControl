using ServerWcfService.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace QualityControl_Client
{
    public class ServiceChannelManager
    {
        private static ICertificateRepository CertificateChannel;
        public ICertificateRepository CertificateRepository
            => CertificateChannel ?? (CertificateChannel = CreateChannel<ICertificateRepository>("http://localhost:8080/CertificateService/"));

        private static IControlMethodDocumentationRepository ControlMethodDocumentationChannel;
        public IControlMethodDocumentationRepository ControlMethodDocumentationRepository
            => ControlMethodDocumentationChannel ?? (ControlMethodDocumentationChannel = CreateChannel<IControlMethodDocumentationRepository>("http://localhost:8080/ControlMethodDocumentationService/"));

        private static ICustomerRepository CustomerChannel;
        public ICustomerRepository CustomerRepository
            => CustomerChannel ?? (CustomerChannel = CreateChannel<ICustomerRepository>("http://localhost:8080/CustomerService/"));

        private static IEquipmentRepository EquipmentChannel;
        public IEquipmentRepository EquipmentRepository
            => EquipmentChannel ?? (EquipmentChannel = CreateChannel<IEquipmentRepository>("http://localhost:8080/EquipmentService/"));

        private static IMaterialRepository MaterialChannel;
        public IMaterialRepository MaterialRepository
            => MaterialChannel ?? (MaterialChannel = CreateChannel<IMaterialRepository>("http://localhost:8080/MaterialService/"));

        private static IRequirementDocumentationRepository RequirementDocumentationChannel;
        public IRequirementDocumentationRepository RequirementDocumentationRepository
            => RequirementDocumentationChannel ?? (RequirementDocumentationChannel = CreateChannel<IRequirementDocumentationRepository>("http://localhost:8080/RequirementDocumentationService/"));

        private static IWeldJointRepository WeldJointChannel;
        public IWeldJointRepository WeldJointRepository
            => WeldJointChannel ?? (WeldJointChannel = CreateChannel<IWeldJointRepository>("http://localhost:8080/WeldJointService/"));

        private static ITemplateRepository TemplateChannel;
        public ITemplateRepository TemplateRepository
            => TemplateChannel ?? (TemplateChannel = CreateChannel<ITemplateRepository>("http://localhost:8080/TemplateService/"));

        private static IControlNameRepository ControlNameChannel;
        public IControlNameRepository ControlNameRepository
            => ControlNameChannel ?? (ControlNameChannel = CreateChannel<IControlNameRepository>("http://localhost:8080/ControlNameService/"));

        private static IComponentRepository ComponentChannel;
        public IComponentRepository ComponentRepository
            => ComponentChannel ?? (ComponentChannel = CreateChannel<IComponentRepository>("http://localhost:8080/ComponentService/"));

        private static IEmployeeRepository EmployeeChannel;
        public IEmployeeRepository EmployeeRepository
            => EmployeeChannel ?? (EmployeeChannel = CreateChannel<IEmployeeRepository>("http://localhost:8080/EmployeeService/"));

        private static IIndustrialObjectRepository IndustrialObjectChannel;
        public IIndustrialObjectRepository IndustrialObjectRepository
            => IndustrialObjectChannel ?? (IndustrialObjectChannel = CreateChannel<IIndustrialObjectRepository>("http://localhost:8080/IndustrialObjectService/"));

        private static IJournalRepository JournalChannel;
        public IJournalRepository JournalRepository
            => JournalChannel ?? (JournalChannel = CreateChannel<IJournalRepository>("http://localhost:8080/JournalService/"));

        private static IImageRepository ImageChannel;
        public IImageRepository ImageRepository
            => ImageChannel ?? (ImageChannel = CreateChannel<IImageRepository>("http://localhost:8080/ImageService/"));

        private static IUserRepository UserChannel;
        public IUserRepository UserRepository
            => UserChannel ?? (UserChannel = CreateChannel<IUserRepository>("http://localhost:8080/UserService/"));

        private static IRoleRepository RoleChannel;
        public IRoleRepository RoleRepository
            => RoleChannel ?? (RoleChannel = CreateChannel<IRoleRepository>("http://localhost:8080/RoleService/"));

        private static ServiceChannelManager instance;

        public static ServiceChannelManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ServiceChannelManager();
                    CertificateChannel = CreateChannel<ICertificateRepository>("http://localhost:8080/CertificateService/");
                    ControlMethodDocumentationChannel = CreateChannel<IControlMethodDocumentationRepository>("http://localhost:8080/ControlMethodDocumentationService/");
                    CustomerChannel = CreateChannel<ICustomerRepository>("http://localhost:8080/CustomerService/");
                    EquipmentChannel = CreateChannel<IEquipmentRepository>("http://localhost:8080/EquipmentService/");
                    MaterialChannel = CreateChannel<IMaterialRepository>("http://localhost:8080/MaterialService/");
                    RequirementDocumentationChannel = CreateChannel<IRequirementDocumentationRepository>("http://localhost:8080/RequirementDocumentationService/");
                    WeldJointChannel = CreateChannel<IWeldJointRepository>("http://localhost:8080/WeldJointService/");
                    TemplateChannel = CreateChannel<ITemplateRepository>("http://localhost:8080/TemplateService/");
                    ControlNameChannel = CreateChannel<IControlNameRepository>("http://localhost:8080/ControlNameService/");
                    ComponentChannel = CreateChannel<IComponentRepository>("http://localhost:8080/ComponentService/");
                    EmployeeChannel = CreateChannel<IEmployeeRepository>("http://localhost:8080/EmployeeService/");
                    IndustrialObjectChannel = CreateChannel<IIndustrialObjectRepository>("http://localhost:8080/IndustrialObjectService/");
                    JournalChannel = CreateChannel<IJournalRepository>("http://localhost:8080/JournalService/");
                    ImageChannel = CreateChannel<IImageRepository>("http://localhost:8080/ImageService/");
                    UserChannel = CreateChannel<IUserRepository>("http://localhost:8080/UserService/");
                    RoleChannel = CreateChannel<IRoleRepository>("http://localhost:8080/RoleService/");
                }
                return instance;
            }
        }
        private static T CreateChannel<T>(string serviceAddress)
        {
            EndpointIdentity spn = EndpointIdentity.CreateSpnIdentity("localhost"); // dns
            Uri uri = new Uri(serviceAddress);
            var address = new EndpointAddress(uri, spn);
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MaxReceivedMessageSize = 2147483647;
            ChannelFactory<T> factory = new ChannelFactory<T>(binding, address);
            return factory.CreateChannel();
        }

        public void CloseChannels<T>(T channel)
        {
            
        }



            
    }
}
