using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface ICustomerRepository : IRepository<DalCustomer>
    {
        DalCustomer GetCustomerByOrganization(string organization);
        DalCustomer GetCustomerByAddress(string address);
        DalCustomer GetCustomerByPhone(string phone);
        DalCustomer GetCustomerByContract(string contract);
    }
}
