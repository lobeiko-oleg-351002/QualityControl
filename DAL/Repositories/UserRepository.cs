using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;
using AutoMapper;
using DAL.Mapping;

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
            var ormEntity = context.Users.FirstOrDefault(entity => entity.login == login);
            if (ormEntity != null)
            {
                if (ormEntity.password.Equals(password))
                {
                    return mapper.MapToDal(ormEntity);
                }
            }
            return null;
            
        }


        public DalUser GetUserByLogin(string login)
        {
            var ormEntity = context.Users.FirstOrDefault(entity => entity.login == login);
            if (ormEntity == null) return null;
            return mapper.MapToDal(ormEntity);
        }

        UserMapper mapper = new UserMapper();

        public new void Delete(DalUser entity)
        {
            var ormEntity = context.Set<User>().Single(User => User.id == entity.Id);
            context.Set<User>().Remove(ormEntity);
        }

        public new DalUser Get(int id)
        {
            var ormEntity = context.Set<User>().FirstOrDefault(User => User.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalUser> GetAll()
        {
            var elements = context.Set<User>().Select(User => User);
            var retElemets = new List<DalUser>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalUser entity)
        {
            var ormEntity = context.Set<User>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new User Create(DalUser entity)
        {
            var res = context.Set<User>().Add(mapper.MapToOrm(entity));
            return res;
        }


    }
}
