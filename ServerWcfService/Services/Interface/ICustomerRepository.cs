using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities;

namespace ServerWcfService.Services.Interface
{
    [ServiceContract]
    public interface ICustomerRepository : IRepository<UilCustomer>
    {
        [OperationContract]
        UilCustomer GetCustomerByOrganization(string organization);

        [OperationContract]
        UilCustomer GetCustomerByAddress(string address);

        [OperationContract]
        UilCustomer GetCustomerByPhone(string phone);

        [OperationContract]
        UilCustomer GetCustomerByContract(string contract);
    }
}
