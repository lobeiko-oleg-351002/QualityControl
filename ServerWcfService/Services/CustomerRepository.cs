using AutoMapper;
using BLL.Entities;
using BLL.Services.Interface;
using ServerWcfService.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities;

namespace ServerWcfService.Services
{
    public class CustomerRepository : Repository<UilCustomer, BllCustomer>, ICustomerRepository
    {
        private readonly ICustomerService customerService;

        public CustomerRepository() : base(UiUnitOfWork.Instance.Customers)
        {
            customerService = UiUnitOfWork.Instance.Customers;
        }

        public UilCustomer GetCustomerByAddress(string address)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllCustomer, UilCustomer>();
            });
            return Mapper.Map<UilCustomer>(customerService.GetCustomerByAddress(address));
        }

        public UilCustomer GetCustomerByContract(string contract)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllCustomer, UilCustomer>();
            });
            return Mapper.Map<UilCustomer>(customerService.GetCustomerByContract(contract));
        }

        public UilCustomer GetCustomerByOrganization(string organization)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllCustomer, UilCustomer>();
            });
            return Mapper.Map<UilCustomer>(customerService.GetCustomerByOrganization(organization));
        }

        public UilCustomer GetCustomerByPhone(string phone)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllCustomer, UilCustomer>();
            });
            return Mapper.Map<UilCustomer>(customerService.GetCustomerByPhone(phone));
        }
    }
}
