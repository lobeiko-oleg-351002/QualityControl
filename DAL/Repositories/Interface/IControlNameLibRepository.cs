using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IControlNameLibRepository : IRepository<DalControlNameLib>
    {
        new ControlNameLib Create(DalControlNameLib entity);
        new void Delete(DalControlNameLib entity);
        new DalControlNameLib Get(int id);
        new IEnumerable<DalControlNameLib> GetAll();
        new void Update(DalControlNameLib entity);
    }
}
