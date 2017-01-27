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
    public class UserRepository : Repository<DalUser, User>, IUserRepository
    {
        private readonly ServiceDB context;
        public UserRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalUser Authorize(string login, string password)
        {
            Mapper.CreateMap<User, DalUser>();
            var ormEntity = context.Users.FirstOrDefault(entity => entity.login == login);
            if (ormEntity != null)
            {
                if (ormEntity.password.Equals(password))
                {
                    return Mapper.Map<DalUser>(ormEntity);
                }
            }
            return null;
            
        }


        public DalUser GetUserByLogin(string login)
        {
            Mapper.CreateMap<User, DalUser>();
            var ormEntity = context.Users.FirstOrDefault(entity => entity.login == login);
            return Mapper.Map<DalUser>(ormEntity);
        }


    }
}
