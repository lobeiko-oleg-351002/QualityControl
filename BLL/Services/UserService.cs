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
    public class UserService : Service<BllUser, DalUser>, IUserService
    {
        private readonly IUnitOfWork uow;
        IUserMapper bllMapper;
        public UserService(IUnitOfWork uow) : base(uow, uow.Users)
        {
            this.uow = uow;
            bllMapper = new UserMapper(uow);
        }

        public BllUser Authorize(string login, string password)
        {
            return bllMapper.MapToBll(uow.Users.Authorize(login, password));
        }

        public BllUser GetUserByLogin(string login)
        {
            return bllMapper.MapToBll(uow.Users.GetUserByLogin(login));
        }

        public new BllUser Create(BllUser entity)
        {
            var testEntity = uow.Users.GetUserByLogin(entity.Login);
            if (testEntity == null)
            {
                uow.Users.Create(bllMapper.MapToDal(entity));
                uow.Commit();
                return entity;
            }
            return null;
        }

        public override IEnumerable<BllUser> GetAll()
        {
            var elements = uow.Users.GetAll();
            var retElemets = new List<BllUser>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }

        public override void Delete(BllUser entity)
        {
            uow.Users.Delete(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Update(BllUser entity)
        {
            uow.Users.Update(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override BllUser Get(int id)
        {
            DalUser dalEntity = uow.Users.Get(id);
            return bllMapper.MapToBll(dalEntity);
        }

        //private BllUser MapDalToBll(DalUser dalEntity)
        //{
        //    Mapper.Initialize(cfg =>
        //    {
        //        cfg.CreateMap<DalUser, BllUser>();
        //        cfg.CreateMap<DalEmployee, BllEmployee>();
        //        cfg.CreateMap<DalRole, BllRole>();
        //    });

        //    BllUser bllEntity = Mapper.Map<BllUser>(dalEntity);
        //    if (bllEntity != null)
        //    {
        //        EmployeeService employeeService = new EmployeeService(uow);
        //        RoleService roleService = new RoleService(uow);
        //        bllEntity.Employee = dalEntity.Employee_id != null ? employeeService.Get((int)dalEntity.Employee_id) : null;
        //        bllEntity.Role = dalEntity.Role_id != null ? roleService.Get((int)dalEntity.Role_id) : null;
        //    }
        //    return bllEntity;
        //}

        //private DalUser MapBllToDal(BllUser entity)
        //{
        //    Mapper.Initialize(cfg =>
        //    {
        //        cfg.CreateMap<BllUser, DalUser>();
        //    });
        //    DalUser dalEntity = Mapper.Map<DalUser>(entity);
        //    dalEntity.Employee_id = entity.Employee != null ? entity.Employee.Id : (int?)null;
        //    dalEntity.Role_id = entity.Role != null ? entity.Role.Id : 0;
        //    return dalEntity;
        //}
    }
}
