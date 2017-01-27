namespace ORM
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ServiceDB : DbContext
    {
        public ServiceDB()
            : base("name=ServiceDB")
        {
        }

        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<CertificateLib> CertificateLibs { get; set; }
        public virtual DbSet<Component> Components { get; set; }
        public virtual DbSet<ComponentLib> ComponentLibs { get; set; }
        public virtual DbSet<Control> Controls { get; set; }
        public virtual DbSet<ControlMethodDocumentation> ControlMethodDocumentations { get; set; }
        public virtual DbSet<ControlMethodDocumentationLib> ControlMethodDocumentationLibs { get; set; }
        public virtual DbSet<ControlMethodsLib> ControlMethodsLibs { get; set; }
        public virtual DbSet<ControlName> ControlNames { get; set; }
        public virtual DbSet<ControlNameLib> ControlNameLibs { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeLib> EmployeeLibs { get; set; }
        public virtual DbSet<Equipment> Equipments { get; set; }
        public virtual DbSet<EquipmentLib> EquipmentLibs { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<ImageLib> ImageLibs { get; set; }
        public virtual DbSet<IndustrialObject> IndustrialObjects { get; set; }
        public virtual DbSet<Journal> Journals { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<RequirementDocumentation> RequirementDocumentations { get; set; }
        public virtual DbSet<RequirementDocumentationLib> RequirementDocumentationLibs { get; set; }
        public virtual DbSet<ResultLib> ResultLibs { get; set; }
        public virtual DbSet<Results> Results { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SelectedCertificate> SelectedCertificates { get; set; }
        public virtual DbSet<SelectedComponent> SelectedComponents { get; set; }
        public virtual DbSet<SelectedControlMethodDocumentation> SelectedControlMethodDocumentations { get; set; }
        public virtual DbSet<SelectedControlName> SelectedControlNames { get; set; }
        public virtual DbSet<SelectedEmployee> SelectedEmployees { get; set; }
        public virtual DbSet<SelectedEquipment> SelectedEquipments { get; set; }
        public virtual DbSet<SelectedRequirementDocumentation> SelectedRequirementDocumentations { get; set; }
        public virtual DbSet<Template> Templates { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WeldJoint> WeldJoints { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Certificate>()
                .HasMany(e => e.SelectedCertificate)
                .WithOptional(e => e.Certificate)
                .HasForeignKey(e => e.certificate_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<CertificateLib>()
                .HasMany(e => e.SelectedCertificate)
                .WithOptional(e => e.CertificateLib)
                .HasForeignKey(e => e.certificate_lib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Component>()
                .HasMany(e => e.Journal)
                .WithOptional(e => e.Component)
                .HasForeignKey(e => e.component_id);

            modelBuilder.Entity<Component>()
                .HasMany(e => e.SelectedComponent)
                .WithOptional(e => e.Component)
                .HasForeignKey(e => e.component_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ComponentLib>()
                .HasMany(e => e.IndustrialObject)
                .WithOptional(e => e.ComponentLib)
                .HasForeignKey(e => e.component_lib_id);

            modelBuilder.Entity<ComponentLib>()
                .HasMany(e => e.SelectedComponent)
                .WithOptional(e => e.ComponentLib)
                .HasForeignKey(e => e.component_lib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ControlMethodDocumentation>()
                .HasMany(e => e.SelectedControlMethodDocumentation)
                .WithOptional(e => e.ControlMethodDocumentation)
                .HasForeignKey(e => e.controlMethodDocumentation_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ControlMethodDocumentationLib>()
                .HasMany(e => e.Control)
                .WithOptional(e => e.ControlMethodDocumentationLib)
                .HasForeignKey(e => e.control_method_documentation_lib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ControlMethodDocumentationLib>()
                .HasMany(e => e.SelectedControlMethodDocumentation)
                .WithOptional(e => e.ControlMethodDocumentationLib)
                .HasForeignKey(e => e.controlMethodDocumentationLib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ControlMethodsLib>()
                .HasMany(e => e.Control)
                .WithOptional(e => e.ControlMethodsLib)
                .HasForeignKey(e => e.controlMethodsLib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ControlMethodsLib>()
                .HasMany(e => e.Journal)
                .WithOptional(e => e.ControlMethodsLib)
                .HasForeignKey(e => e.controlMethodsLib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ControlName>()
                .HasMany(e => e.Control)
                .WithOptional(e => e.ControlName)
                .HasForeignKey(e => e.control_name_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ControlName>()
                .HasMany(e => e.ControlMethodDocumentation)
                .WithOptional(e => e.ControlName)
                .HasForeignKey(e => e.controlName_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ControlName>()
                .HasMany(e => e.SelectedControlName)
                .WithOptional(e => e.ControlName)
                .HasForeignKey(e => e.controlName_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ControlNameLib>()
                .HasMany(e => e.SelectedControlName)
                .WithOptional(e => e.ControlNameLib)
                .HasForeignKey(e => e.controlNameLib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ControlNameLib>()
                .HasMany(e => e.Template)
                .WithOptional(e => e.ControlNameLib)
                .HasForeignKey(e => e.control_name_lib_id);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Journal)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.customer_id);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Certificate)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.employee_id);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.SelectedEmployee)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.employee_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<EmployeeLib>()
                .HasMany(e => e.Control)
                .WithOptional(e => e.EmployeeLib)
                .HasForeignKey(e => e.employeeLib_id);

            modelBuilder.Entity<EmployeeLib>()
                .HasMany(e => e.SelectedEmployee)
                .WithOptional(e => e.EmployeeLib)
                .HasForeignKey(e => e.employeeLib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Equipment>()
                .Property(e => e.isChecked)
                .IsFixedLength();

            modelBuilder.Entity<Equipment>()
                .HasMany(e => e.SelectedEquipment)
                .WithOptional(e => e.Equipment)
                .HasForeignKey(e => e.equipment_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<EquipmentLib>()
                .HasMany(e => e.Template)
                .WithOptional(e => e.EquipmentLib)
                .HasForeignKey(e => e.equipment_lib_id);

            modelBuilder.Entity<EquipmentLib>()
                .HasMany(e => e.Control)
                .WithOptional(e => e.EquipmentLib)
                .HasForeignKey(e => e.equipmentLib_id);

            modelBuilder.Entity<EquipmentLib>()
                .HasMany(e => e.SelectedEquipment)
                .WithOptional(e => e.EquipmentLib)
                .HasForeignKey(e => e.equipmentLib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ImageLib>()
                .HasMany(e => e.Control)
                .WithOptional(e => e.ImageLib)
                .HasForeignKey(e => e.image_lib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ImageLib>()
                .HasMany(e => e.Image)
                .WithOptional(e => e.ImageLib)
                .HasForeignKey(e => e.image_lib_Id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ImageLib>()
                .HasMany(e => e.Template)
                .WithOptional(e => e.ImageLib)
                .HasForeignKey(e => e.image_lib_id);

            modelBuilder.Entity<IndustrialObject>()
                .HasMany(e => e.Journal)
                .WithOptional(e => e.IndustrialObject)
                .HasForeignKey(e => e.industrial_object_id);

            modelBuilder.Entity<Material>()
                .HasMany(e => e.Template)
                .WithOptional(e => e.Material)
                .HasForeignKey(e => e.material_id);

            modelBuilder.Entity<RequirementDocumentation>()
                .HasMany(e => e.SelectedRequirementDocumentation)
                .WithOptional(e => e.RequirementDocumentation)
                .HasForeignKey(e => e.requirementDocumentation_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<RequirementDocumentationLib>()
                .HasMany(e => e.Control)
                .WithOptional(e => e.RequirementDocumentationLib)
                .HasForeignKey(e => e.requirement_documentation_lib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<RequirementDocumentationLib>()
                .HasMany(e => e.Template)
                .WithOptional(e => e.RequirementDocumentationLib)
                .HasForeignKey(e => e.requirementDocumentation_lib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<RequirementDocumentationLib>()
                .HasMany(e => e.SelectedRequirementDocumentation)
                .WithOptional(e => e.RequirementDocumentationLib)
                .HasForeignKey(e => e.requirementDocumentationLib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ResultLib>()
                .HasMany(e => e.Control)
                .WithOptional(e => e.ResultLib)
                .HasForeignKey(e => e.resultLib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ResultLib>()
                .HasMany(e => e.Results)
                .WithOptional(e => e.ResultLib)
                .HasForeignKey(e => e.resultLib_id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Role>()
                .HasMany(e => e.User)
                .WithRequired(e => e.Role)
                .HasForeignKey(e => e.role_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Template>()
                .HasMany(e => e.Component)
                .WithOptional(e => e.Template)
                .HasForeignKey(e => e.template_id);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Certificate)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.creator_id);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Component)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.creator_id);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ControlMethodDocumentation)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.creator_id);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Customer)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.creator_id);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Equipment)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.creator_id);

            modelBuilder.Entity<User>()
                .HasMany(e => e.IndustrialObject)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.creator_id);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Material)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.creator_id);

            modelBuilder.Entity<User>()
                .HasMany(e => e.RequirementDocumentation)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.creator_id);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Template)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.creator_id);

            modelBuilder.Entity<User>()
                .HasMany(e => e.WeldJoint)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.creator_id);

            modelBuilder.Entity<WeldJoint>()
                .HasMany(e => e.Journal)
                .WithOptional(e => e.WeldJoint)
                .HasForeignKey(e => e.weld_joint_id);

            modelBuilder.Entity<WeldJoint>()
                .HasMany(e => e.Template)
                .WithOptional(e => e.WeldJoint)
                .HasForeignKey(e => e.weld_joint_id);
        }
    }
}
