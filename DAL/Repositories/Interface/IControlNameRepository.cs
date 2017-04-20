using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IControlNameRepository : IRepository<DalControlName>
    {
        DalControlName GetControlNameByName(string name);
        new ControlName Create(DalControlName entity);
        new void Delete(DalControlName entity);
        new DalControlName Get(int id);
        new IEnumerable<DalControlName> GetAll();
        new void Update(DalControlName entity);
    }
}
