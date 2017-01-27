using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface ISelectedEmployeeRepository : IRepository<DalSelectedEmployee>
    {
        IEnumerable<DalSelectedEmployee> GetEmployeesByLibId(int id);
        new SelectedEmployee Create(DalSelectedEmployee entity);
    }
}
