using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IResultLibRepository : IRepository<DalResultLib>
    {
        new ResultLib Create(DalResultLib entity);
        new void Delete(DalResultLib entity);
        new DalResultLib Get(int id);
        new IEnumerable<DalResultLib> GetAll();
        new void Update(DalResultLib entity);
    }
}
