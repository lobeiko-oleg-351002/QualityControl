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
    public class RoleMapper : IRoleMapper
    {
        public DalRole MapToDal(Role entity)
        {
            return new DalRole
            {
                Id = entity.id,
                Name = entity.name
            };
        }

        public Role MapToOrm(DalRole entity)
        {
            return new Role
            {
                id = entity.Id,
                name = entity.Name
            };
        }
    }
}
