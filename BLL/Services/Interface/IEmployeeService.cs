using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IEmployeeService : IService<BllEmployee>
    {
        IEnumerable<BllEmployee> GetEmployeesByName(string name);
        IEnumerable<BllEmployee> GetEmployeesByFatherName(string name);
        IEnumerable<BllEmployee> GetEmployeesBySirname(string name);
        IEnumerable<BllEmployee> GetEmployeesByFunction(string function);
    }
}
