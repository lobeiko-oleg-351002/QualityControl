using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IEmployeeLibRepository : IRepository<DalEmployeeLib>
    {
        new EmployeeLib Create(DalEmployeeLib entity);
        new void Delete(DalEmployeeLib entity);
        new DalEmployeeLib Get(int id);
        new IEnumerable<DalEmployeeLib> GetAll();
        new void Update(DalEmployeeLib entity);
    }
}
