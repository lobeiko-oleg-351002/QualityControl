using DAL.Entities;
using DAL.Mapping.Interfaces;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping
{
    public class UserMapper : IUserMapper
    {
        public DalUser MapToDal(User entity)
        {
            return new DalUser
            {
                Id = entity.id,
                Employee_id = entity.employee_id,
                Login = entity.login,
                ModifiedDate = entity.modifiedDate,
                Password = entity.password,
                Role_id = entity.role_id
            };
        }

        public User MapToOrm(DalUser entity)
        {
            return new User
            {
                id = entity.Id,
                employee_id = entity.Employee_id,
                login = entity.Login,
                modifiedDate = entity.ModifiedDate,
                password = entity.Password,
                role_id = entity.Role_id
            };
        }
    }
}
