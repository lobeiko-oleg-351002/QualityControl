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
    public class UserRepository : Repository<UilUser, BllUser>, IUserRepository
    {
        private readonly IUserService UserService;

        public UserRepository() : base(UiUnitOfWork.Instance.Users)
        {
            UserService = UiUnitOfWork.Instance.Users;
        }

        public override IEnumerable<UilUser> GetAll()
        {
            var elements = UserService.GetAll();
            var retElemets = new List<UilUser>();
            foreach (var element in elements)
            {
                retElemets.Add(MapBllToUil(element));
            }
            return retElemets;
        }

        public override void Create(UilUser entity)
        {
            UserService.Create(MapUilToBll(entity));
        }

        public override void Update(UilUser entity)
        {
            UserService.Update(MapUilToBll(entity));
        }

        public override void Delete(UilUser entity)
        {
            UserService.Delete(MapUilToBll(entity));
        }

        public UilUser Authorize(string login, string password)
        {
            return MapBllToUil(UserService.Authorize(login, password));
        }

        public UilUser MapBllToUil(BllUser bllEntity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllUser, UilUser>();
                cfg.CreateMap<BllEmployee, UilEmployee>();
                cfg.CreateMap<BllRole, UilRole>();
            });
            UilUser uilEntity = Mapper.Map<UilUser>(bllEntity);
            if (uilEntity != null)
            {
                uilEntity.Employee = EmployeeRepository.MapBllToUil(bllEntity.Employee);
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<BllRole, UilRole>();
                });
                uilEntity.Role = Mapper.Map<UilRole>(bllEntity.Role);
            }


            return uilEntity;
        }

        public BllUser MapUilToBll(UilUser entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UilUser, BllUser>();
                cfg.CreateMap<UilEmployee, BllEmployee>();
                cfg.CreateMap<UilRole, BllRole>();
            });

            BllUser bllEntity = Mapper.Map<BllUser>(entity);
            bllEntity.Employee = EmployeeRepository.MapUilToBll(entity.Employee);

            return bllEntity;
        }

        public UilUser CreateWithFeedBack(UilUser entity)
        {
            return MapBllToUil(UserService.Create(MapUilToBll(entity)));
        }
    }
}
