using AutoMapper;
using BLL.Entities;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmployeeLibService : Service<BllEmployeeLib, DalEmployeeLib>, IEmployeeLibService
    {
        private readonly IUnitOfWork uow;

        public EmployeeLibService(IUnitOfWork uow) : base(uow, uow.EmployeeLibs)
        {
            this.uow = uow;
        }

        public new BllEmployeeLib Create(BllEmployeeLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllSelectedEmployee, DalSelectedEmployee>();
                cfg.CreateMap<EmployeeLib, DalEmployeeLib>();
                cfg.CreateMap<BllEmployeeLib, DalEmployeeLib>();
                cfg.CreateMap<DalEmployeeLib, BllEmployeeLib>();
            });
            var ormEntity = uow.EmployeeLibs.Create(Mapper.Map<DalEmployeeLib>(entity));
            uow.Commit();
            entity.Id = ormEntity.id;
            foreach (var Employee in entity.SelectedEmployee)
            {
                Mapper.CreateMap<BllSelectedEmployee, DalSelectedEmployee>();
                var dalEmployee = Mapper.Map<DalSelectedEmployee>(Employee);
                dalEmployee.EmployeeLib_id = entity.Id;
                var ormEmployee = uow.SelectedEmployees.Create(dalEmployee);
                uow.Commit();
                Employee.Id = ormEmployee.id;
            }

            return entity;
        }

        public override BllEmployeeLib Get(int id)
        {
            Mapper.CreateMap<DalEmployeeLib, BllEmployeeLib>();
            return MapDalToBll(uow.EmployeeLibs.Get(id));
        }

        public new BllEmployeeLib Update(BllEmployeeLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllSelectedEmployee, DalSelectedEmployee>();
                cfg.CreateMap<EmployeeLib, DalEmployeeLib>();
                cfg.CreateMap<BllEmployeeLib, DalEmployeeLib>();
                cfg.CreateMap<DalEmployeeLib, BllEmployeeLib>();
            });
            foreach (var Employee in entity.SelectedEmployee)
            {
                if (Employee.Id == 0)
                {
                    var dalEmployee = Mapper.Map<DalSelectedEmployee>(Employee);
                    dalEmployee.EmployeeLib_id = entity.Id;
                    var ormEmployee = uow.SelectedEmployees.Create(dalEmployee);
                    uow.Commit();
                    Employee.Id = ormEmployee.id;
                }
            }
            var EmployeesWithLibId = uow.SelectedEmployees.GetEmployeesByLibId(entity.Id);
            foreach (var Employee in EmployeesWithLibId)
            {
                bool isTrashEmployee = true;
                foreach (var selectedEmployee in entity.SelectedEmployee)
                {
                    if (Employee.Id == selectedEmployee.Id)
                    {
                        isTrashEmployee = false;
                        break;
                    }
                }
                if (isTrashEmployee == true)
                {
                    uow.SelectedEmployees.Delete(Employee);
                }
            }
            uow.Commit();

            return entity;
        }

        private BllEmployeeLib MapDalToBll(DalEmployeeLib dalEntity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DalEmployeeLib, BllEmployeeLib>();
                cfg.CreateMap<DalEmployee, BllEmployee>();
                cfg.CreateMap<DalSelectedEmployee, BllSelectedEmployee>();
            });
            BllEmployeeLib bllEntity = Mapper.Map<BllEmployeeLib>(dalEntity);

            SelectedEmployeeService selectedEmployeeService = new SelectedEmployeeService(uow);
            EmployeeService EmployeeService = new EmployeeService(uow);
            foreach (var Employee in uow.SelectedEmployees.GetEmployeesByLibId(dalEntity.Id))
            {
                var bllEmployee = Employee.Employee_id != null ? EmployeeService.Get((int)Employee.Employee_id) : null;
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<DalEmployee, BllEmployee>();
                    cfg.CreateMap<DalSelectedEmployee, BllSelectedEmployee>();
                });
                var bllSelectedEmployee = Mapper.Map<BllSelectedEmployee>(Employee);
                bllSelectedEmployee.Employee = bllEmployee;
                bllEntity.SelectedEmployee.Add(bllSelectedEmployee);

            }
            return bllEntity;
        }
    }
}
