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
    public class RoleService : Service<BllRole, DalRole>, IRoleService
    {
        private readonly IUnitOfWork uow;

        public RoleService(IUnitOfWork uow) : base(uow, uow.Roles)
        {
            this.uow = uow;
        }

        public BllRole GetRoleByName(string name)
        {
            Mapper.CreateMap<DalRole, BllRole>();
            return Mapper.Map<BllRole>(uow.Roles.GetRoleByName(name));
        }
    }
}
