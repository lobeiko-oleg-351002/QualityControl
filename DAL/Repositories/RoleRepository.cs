using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;
using AutoMapper;

namespace DAL.Repositories
{
    public class RoleRepository : Repository<DalRole, Role>, IRoleRepository
    {
        private readonly ServiceDB context;
        public RoleRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalRole GetRoleByName(string name)
        {
            Mapper.CreateMap<Role, DalRole>();
            var ormEntity = context.Roles.FirstOrDefault(entity => entity.name == name);
            return Mapper.Map<DalRole>(ormEntity);
        }
    }
}
