using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;


namespace QualityControl_Server
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        ServiceInstaller certificate;
        ServiceInstaller component;
        ServiceInstaller controlMethodDocumentation;
        ServiceInstaller controlName;
        ServiceInstaller control;
        ServiceInstaller customer;
        ServiceInstaller employee;
        ServiceInstaller equipment;
        ServiceInstaller image;
        ServiceInstaller industrialObject;
        ServiceInstaller journal;
        ServiceInstaller material;
        ServiceInstaller requirementDocumentation;
        ServiceInstaller result;
        ServiceInstaller template;
        ServiceInstaller weldJoint;

        public ProjectInstaller()
        {
            InitializeComponent();
            ServiceProcessInstaller serviceProcessInstaller =
                  new ServiceProcessInstaller();


            //# Service Account Information
            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;
            this.Installers.Add(serviceProcessInstaller);

            certificate = InitService("CertificateRepository");
            Installers.Add(certificate);

            component = InitService("ComponentRepository");
            Installers.Add(component);

            controlMethodDocumentation = InitService("ControlMethodDocumentationRepository");
            Installers.Add(controlMethodDocumentation);

            controlName = InitService("ControlNameRepository");
            Installers.Add(controlName);

            control = InitService("ControlRepository");
            Installers.Add(control);

            customer = InitService("CustomerRepository");
            Installers.Add(customer);

            employee = InitService("EmployeeRepository");
            Installers.Add(employee);

            equipment = InitService("EquipmentRepository");
            Installers.Add(equipment);

            image = InitService("ImageRepository");
            Installers.Add(image);

            industrialObject = InitService("IndustrialObjectRepository");
            Installers.Add(industrialObject);

            journal = InitService("JournalRepository");
            Installers.Add(journal);

            material = InitService("MaterialRepository");
            Installers.Add(material);

            requirementDocumentation = InitService("RequirementDocumentationRepository");
            Installers.Add(requirementDocumentation);

            result = InitService("ResultRepository");
            Installers.Add(result);

            template = InitService("TemplateRepository");
            Installers.Add(template);

            weldJoint = InitService("WeldJointRepository");
            Installers.Add(weldJoint);
        }

        protected override void OnAfterInstall(IDictionary savedState)
        {
            new ServiceController(certificate.ServiceName).Start();
            new ServiceController(component.ServiceName).Start();
            new ServiceController(controlMethodDocumentation.ServiceName).Start();
            new ServiceController(controlName.ServiceName).Start();
            new ServiceController(control.ServiceName).Start();
            new ServiceController(customer.ServiceName).Start();
            new ServiceController(employee.ServiceName).Start();
            new ServiceController(equipment.ServiceName).Start();
            new ServiceController(image.ServiceName).Start();
            new ServiceController(industrialObject.ServiceName).Start();
            new ServiceController(journal.ServiceName).Start();
            new ServiceController(material.ServiceName).Start();
            new ServiceController(requirementDocumentation.ServiceName).Start();
            new ServiceController(result.ServiceName).Start();
            new ServiceController(template.ServiceName).Start();
            new ServiceController(weldJoint.ServiceName).Start();
        }

        private ServiceInstaller InitService(string Name)
        {
            ServiceInstaller serviceInstaller = new ServiceInstaller();
            //# Service Information
            serviceInstaller.DisplayName = "QualityControl Service";
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.ServiceName = Name;

            return serviceInstaller;
        }
    }
}
