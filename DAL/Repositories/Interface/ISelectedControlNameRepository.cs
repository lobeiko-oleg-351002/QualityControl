using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface ISelectedControlNameRepository : IRepository<DalSelectedControlName>
    {
        IEnumerable<DalSelectedControlName> GetControlNamesByLibId(int id);
        new SelectedControlName Create(DalSelectedControlName entity);
        new void Delete(DalSelectedControlName entity);
        new DalSelectedControlName Get(int id);
        new IEnumerable<DalSelectedControlName> GetAll();
        new void Update(DalSelectedControlName entity);
    }
}
