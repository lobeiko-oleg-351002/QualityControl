using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IUserRepository : IRepository<DalUser>
    {
        DalUser GetUserByLogin(string login);
        DalUser Authorize(string login, string password);
        new User Create(DalUser entity);
        new void Delete(DalUser entity);
        new DalUser Get(int id);
        new IEnumerable<DalUser> GetAll();
        new void Update(DalUser entity);
    }
}
