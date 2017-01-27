using AutoMapper;
using BLL.Entities;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CustomerService : Service<BllCustomer, DalCustomer>, ICustomerService
    {
        private readonly IUnitOfWork uow;

        public CustomerService(IUnitOfWork uow) : base(uow, uow.Customers)
        {
            this.uow = uow;
        }

        public BllCustomer GetCustomerByAddress(string address)
        {
            Mapper.CreateMap<DalCustomer, BllCustomer>();
            return Mapper.Map<BllCustomer>(uow.Customers.GetCustomerByAddress(address));
        }
        public BllCustomer GetCustomerByContract(string contract)
        {
            Mapper.CreateMap<DalCustomer, BllCustomer>();
            return Mapper.Map<BllCustomer>(uow.Customers.GetCustomerByContract(contract));
        }

        public BllCustomer GetCustomerByOrganization(string organization)
        {
            Mapper.CreateMap<DalCustomer, BllCustomer>();
            return Mapper.Map<BllCustomer>(uow.Customers.GetCustomerByOrganization(organization));
        }

        public BllCustomer GetCustomerByPhone(string phone)
        {
            Mapper.CreateMap<DalCustomer, BllCustomer>();
            return Mapper.Map<BllCustomer>(uow.Customers.GetCustomerByPhone(phone));
        }
    }
}
