using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IRoleRepository : IRepository<DalRole>
    {
        DalRole GetRoleByName(string name);
        new Role Create(DalRole entity);
        new void Delete(DalRole entity);
        new DalRole Get(int id);
        new IEnumerable<DalRole> GetAll();
        new void Update(DalRole entity);
    }
}
