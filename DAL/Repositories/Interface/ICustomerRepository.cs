using DAL.Entities;
using ORM;
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

        new Customer Create(DalCustomer entity);
        new void Delete(DalCustomer entity);
        new DalCustomer Get(int id);
        new IEnumerable<DalCustomer> GetAll();
        new void Update(DalCustomer entity);
    }
}
