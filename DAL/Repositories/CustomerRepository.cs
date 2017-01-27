using AutoMapper;
using DAL.Entities;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CustomerRepository : Repository<DalCustomer, Customer>, ICustomerRepository
    {
        private readonly ServiceDB context;
        public CustomerRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalCustomer GetCustomerByAddress(string address)
        {
            Mapper.CreateMap<Customer, DalCustomer>();
            var ormEntity = context.Customers.FirstOrDefault(entity => entity.address == address);
            return Mapper.Map<DalCustomer>(ormEntity);
        }

        public DalCustomer GetCustomerByContract(string contract)
        {
            Mapper.CreateMap<Customer, DalCustomer>();
            var ormEntity = context.Customers.FirstOrDefault(entity => entity.contract == contract);
            return Mapper.Map<DalCustomer>(ormEntity);
        }

        public DalCustomer GetCustomerByOrganization(string organization)
        {
            Mapper.CreateMap<Customer, DalCustomer>();
            var ormEntity = context.Customers.FirstOrDefault(entity => entity.organization == organization);
            return Mapper.Map<DalCustomer>(ormEntity);
        }

        public DalCustomer GetCustomerByPhone(string phone)
        {
            Mapper.CreateMap<Customer, DalCustomer>();
            var ormEntity = context.Customers.FirstOrDefault(entity => entity.phone == phone);
            return Mapper.Map<DalCustomer>(ormEntity);
        }
    }
}
