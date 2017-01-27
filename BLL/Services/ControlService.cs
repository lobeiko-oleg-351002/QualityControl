using AutoMapper;
using BLL.Entities;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ControlService : Service<BllControl, DalControl>, IControlService
    {
        private readonly IUnitOfWork uow;

        public ControlService(IUnitOfWork uow) : base(uow, uow.Controls)
        {
            this.uow = uow;
        }

        public IEnumerable<BllControl> GetAllControlled()
        {
            Mapper.CreateMap<DalControl, BllControl>();
            var elements = uow.Controls.GetAllControlled();
            var retElemets = new List<BllControl>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<BllControl>(element));
            }
            return retElemets;
        }

        public IEnumerable<BllControl> GetAllUncontrolled()
        {
            Mapper.CreateMap<DalControl, BllControl>();
            var elements = uow.Controls.GetAllUncontrolled();
            var retElemets = new List<BllControl>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<BllControl>(element));
            }
            return retElemets;
        }

        public BllControl GetControlByNumber(int number)
        {
            Mapper.CreateMap<DalControl, BllControl>();
            return Mapper.Map<BllControl>(uow.Controls.GetControlByNumber(number));
        }

        public new BllControl Create(BllControl entity)
        {
            IImageLibService imageLibService = new ImageLibService(uow);
            IEquipmentLibService equipmentLibService = new EquipmentLibService(uow);
            IControlMethodDocumentationLibService controlMethodDocumentationLibService = new ControlMethodDocumentationLibService(uow);
            IRequirementDocumentationLibService requirementDocumentationLibService = new RequirementDocumentationLibService(uow);
            IEmployeeLibService employeeLibService = new EmployeeLibService(uow);
            IResultLibService resultLibService = new ResultLibService(uow);

            var imageLib = imageLibService.Create(entity.ImageLib);
            entity.ImageLib = imageLib;
            entity.EquipmentLib = equipmentLibService.Create(entity.EquipmentLib);
            entity.ControlMethodDocumentationLib = controlMethodDocumentationLibService.Create(entity.ControlMethodDocumentationLib);
            entity.RequirementDocumentationLib = requirementDocumentationLibService.Create(entity.RequirementDocumentationLib);
            entity.EmployeeLib = employeeLibService.Create(entity.EmployeeLib);
            entity.ResultLib = resultLibService.Create(entity.ResultLib);

            return entity;

        }

        public override void Update(BllControl entity)
        {
            ResultLibService resultLibService = new ResultLibService(uow);
            resultLibService.Update(entity.ResultLib);
            EquipmentLibService equipmentLibService = new EquipmentLibService(uow);
            equipmentLibService.Update(entity.EquipmentLib);
            uow.Controls.Update(MapBllToDal(entity));
            ControlMethodDocumentationLibService controlMethodDocumentationLibService = new ControlMethodDocumentationLibService(uow);
            controlMethodDocumentationLibService.Update(entity.ControlMethodDocumentationLib);
            RequirementDocumentationLibService requirementDocumentationLibService = new RequirementDocumentationLibService(uow);
            requirementDocumentationLibService.Update(entity.RequirementDocumentationLib);
            EmployeeLibService employeeLibService = new EmployeeLibService(uow);
            employeeLibService.Update(entity.EmployeeLib);
            uow.Commit();
        }

        public override void Delete(BllControl entity)
        {
            uow.Controls.Delete(MapBllToDal(entity));
            uow.Commit();
        }

        public override IEnumerable<BllControl> GetAll()
        {
            var elements = uow.Controls.GetAll();
            var retElemets = new List<BllControl>();
            foreach (var element in elements)
            {
                retElemets.Add(MapDalToBll(element));
            }
            return retElemets;
        }

        public override BllControl Get(int id)
        {
            return MapDalToBll(uow.Controls.Get(id));
        }

        public static DalControl MapBllToDal(BllControl entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllControl, DalControl>();
            });

            DalControl dalEntity = Mapper.Map<DalControl>(entity);
            dalEntity.Control_method_documentation_lib_id = entity.ControlMethodDocumentationLib != null ? entity.ControlMethodDocumentationLib.Id : (int?)null;
           // dalEntity.ControlMethodsLib_id = entity.ControlMethodsLib != null ? entity.ControlMethodsLib.Id : (int?)null;
            dalEntity.Control_name_id = entity.ControlName != null ? entity.ControlName.Id : (int?)null;
            dalEntity.EmployeeLib_id = entity.EmployeeLib != null ? entity.EmployeeLib.Id : (int?)null;
            dalEntity.EquipmentLib_id = entity.EquipmentLib != null ? entity.EquipmentLib.Id : (int?)null;
            dalEntity.Image_lib_id = entity.ImageLib != null ? entity.ImageLib.Id : (int?)null;
            dalEntity.Requirement_documentation_lib_id = entity.RequirementDocumentationLib != null ? entity.RequirementDocumentationLib.Id : (int?)null;
            dalEntity.ResultLib_id = entity.ResultLib != null ? entity.ResultLib.Id : (int?)null;
            dalEntity.Is_controlled = entity.Is_сontrolled;
            return dalEntity;
        }

        public BllControl MapDalToBll(DalControl entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DalControl, BllControl>();
            });
            BllControl bllControl = Mapper.Map<BllControl>(entity);
            ControlMethodDocumentationLibService controlMethodDocumentationLibService = new ControlMethodDocumentationLibService(uow);
            ControlMethodsLibService controlMethodsLibService = new ControlMethodsLibService(uow);
            ControlNameService controlNameService = new ControlNameService(uow);
            EmployeeLibService employeeLibService = new EmployeeLibService(uow);
            EquipmentLibService equipmentService = new EquipmentLibService(uow);
            ImageLibService imageLibService = new ImageLibService(uow);
            RequirementDocumentationLibService requirementDocumentationLibService = new RequirementDocumentationLibService(uow);
            ResultLibService resultLibService = new ResultLibService(uow);
            bllControl.ControlMethodDocumentationLib = entity.Control_method_documentation_lib_id != null ? controlMethodDocumentationLibService.Get((int)entity.Control_method_documentation_lib_id) : null;
            //bllControl.ControlMethodsLib = entity.ControlMethodsLib_id != null ? controlMethodsLibService.Get((int)entity.ControlMethodsLib_id) : null;
            bllControl.ControlName = entity.Control_name_id != null ? controlNameService.Get((int)entity.Control_name_id) : null;
            bllControl.EmployeeLib = entity.EmployeeLib_id != null ? employeeLibService.Get((int)entity.EmployeeLib_id) : null;
            bllControl.EquipmentLib = entity.EquipmentLib_id != null ? equipmentService.Get((int)entity.EquipmentLib_id) : null;
            bllControl.ImageLib = entity.Image_lib_id != null ? imageLibService.Get((int)entity.Image_lib_id) : null;
            bllControl.RequirementDocumentationLib = entity.Requirement_documentation_lib_id != null ? requirementDocumentationLibService.Get((int)entity.Requirement_documentation_lib_id) : null;
            bllControl.ResultLib = entity.ResultLib_id != null ? resultLibService.Get((int)entity.ResultLib_id) : null;
            bllControl.Is_сontrolled = entity.Is_controlled;
            return bllControl;
        }
    }
}
