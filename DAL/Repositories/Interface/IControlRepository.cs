using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IControlRepository : IRepository<DalControl>
    {
        IEnumerable<DalControl> GetAllControlled();
        IEnumerable<DalControl> GetAllUncontrolled();
        DalControl GetControlByNumber(int number);
        IEnumerable<DalControl> GetControlsByLibId(int id);
        int GetControlCountWithCurrentType(int controlNameId);

        new Control Create(DalControl entity);
        new void Delete(DalControl entity);
        new DalControl Get(int id);
        new IEnumerable<DalControl> GetAll();
        new void Update(DalControl entity);
    }
}
