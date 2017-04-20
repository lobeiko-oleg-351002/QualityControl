using BLL.Entities;
using BLL.Mapping.Interfaces;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class UserMapper : IUserMapper
    {
        IUnitOfWork uow;
        IRoleService roleService;
        IEmployeeService employeeService;
        public UserMapper(IUnitOfWork uow)
        {
            this.uow = uow;
            roleService = new RoleService(uow);
            employeeService = new EmployeeService(uow);
        }

        public DalUser MapToDal(BllUser entity)
        {
            DalUser dalEntity = new DalUser
            {
                Id = entity.Id,
                Login = entity.Login,
                ModifiedDate = entity.ModifiedDate,
                Password = entity.Password,
                Role_id = entity.Role.Id,
                Employee_id = entity.Employee != null ? entity.Employee.Id : (int?)null
            };

            return dalEntity;
        }


        public BllUser MapToBll(DalUser entity)
        {
            BllUser bllEntity = new BllUser
            {
                Id = entity.Id,
                Login = entity.Login,
                ModifiedDate = entity.ModifiedDate,
                Password = entity.Password,
                Role = roleService.Get(entity.Role_id),
                Employee = entity.Employee_id != null ? employeeService.Get(entity.Employee_id.Value) : null
            };

            return bllEntity;
        }
    }
}
