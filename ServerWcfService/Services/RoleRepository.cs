using AutoMapper;
using BLL.Entities;
using BLL.Services.Interface;
using ServerWcfService.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities;

namespace ServerWcfService.Services
{
    public class RoleRepository : Repository<UilRole, BllRole>, IRoleRepository
    {
        private readonly IRoleService RoleService;

        public RoleRepository() : base(UiUnitOfWork.Instance.Roles)
        {
            RoleService = UiUnitOfWork.Instance.Roles;
        }

        public UilRole GetRoleByName(string name)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllRole, UilRole>();
            });
            return Mapper.Map<UilRole>(RoleService.GetRoleByName(name));
        }
    }
}
