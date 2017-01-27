using DAL.Entities;
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
    }
}
