using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IUserService : IService<BllUser>
    {
        BllUser GetUserByLogin(string login);
        BllUser Authorize(string login, string password);
        new BllUser Create(BllUser entity);
    }
}
