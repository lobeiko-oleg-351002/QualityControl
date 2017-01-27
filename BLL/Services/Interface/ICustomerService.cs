using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface ICustomerService : IService<BllCustomer>
    {
        BllCustomer GetCustomerByOrganization(string organization);
        BllCustomer GetCustomerByAddress(string address);
        BllCustomer GetCustomerByPhone(string phone);
        BllCustomer GetCustomerByContract(string contract);
    }
}
