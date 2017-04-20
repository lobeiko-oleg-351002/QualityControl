using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
using BLL.Mapping.Interfaces;
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
        IRoleMapper bllMapper = new RoleMapper();
        public RoleService(IUnitOfWork uow) : base(uow, uow.Roles)
        {
            this.uow = uow;
        }

        public override void Create(BllRole entity)
        {
            uow.Roles.Create(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllRole entity)
        {
            uow.Roles.Delete(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override IEnumerable<BllRole> GetAll()
        {
            var elements = uow.Roles.GetAll();
            var retElemets = new List<BllRole>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }

        public override BllRole Get(int id)
        {
            return bllMapper.MapToBll(uow.Roles.Get(id));
        }

        public BllRole GetRoleByName(string name)
        {
            return bllMapper.MapToBll(uow.Roles.GetRoleByName(name));
        }
    }
}
