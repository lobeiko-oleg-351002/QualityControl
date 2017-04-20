using BLL.Entities;
using BLL.Mapping.Interfaces;
using BLL.Services;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class SelectedEmployeeMapper : ISelectedEmployeeMapper
    {
        IUnitOfWork uow;
        public SelectedEmployeeMapper(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public DalSelectedEmployee MapToDal(BllSelectedEmployee entity)
        {
            DalSelectedEmployee dalEntity = new DalSelectedEmployee
            {
                Id = entity.Id,
                Employee_id = entity.Employee.Id,
            };

            return dalEntity;
        }

        public BllSelectedEmployee MapToBll(DalSelectedEmployee entity)
        {
            EmployeeService employeeService = new EmployeeService(uow);
            var bllEmployee = entity.Employee_id != null ? employeeService.Get((int)entity.Employee_id) : null;

            BllSelectedEmployee bllEntity = new BllSelectedEmployee
            {
                Id = entity.Id,
                Employee = bllEmployee
            };

            return bllEntity;
        }
    }
}
