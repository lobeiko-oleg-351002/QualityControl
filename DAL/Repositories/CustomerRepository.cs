using AutoMapper;
using DAL.Entities;
using DAL.Mapping;
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
            var ormEntity = context.Customers.FirstOrDefault(entity => entity.address == address);
            return mapper.MapToDal(ormEntity);
        }

        public DalCustomer GetCustomerByContract(string contract)
        {
            var ormEntity = context.Customers.FirstOrDefault(entity => entity.contract == contract);
            return mapper.MapToDal(ormEntity);
        }

        public DalCustomer GetCustomerByOrganization(string organization)
        {
            var ormEntity = context.Customers.FirstOrDefault(entity => entity.organization == organization);
            return mapper.MapToDal(ormEntity);
        }

        public DalCustomer GetCustomerByPhone(string phone)
        {
            var ormEntity = context.Customers.FirstOrDefault(entity => entity.phone == phone);
            return mapper.MapToDal(ormEntity);
        }

        CustomerMapper mapper = new CustomerMapper();

        public void Delete(DalCustomer entity)
        {
            var ormEntity = context.Set<Customer>().Single(Customer => Customer.id == entity.Id);
            context.Set<Customer>().Remove(ormEntity);
        }

        public new DalCustomer Get(int id)
        {
            var ormEntity = context.Set<Customer>().FirstOrDefault(Customer => Customer.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalCustomer> GetAll()
        {
            var elements = context.Set<Customer>().Select(Customer => Customer);
            var retElemets = new List<DalCustomer>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalCustomer entity)
        {
            var ormEntity = context.Set<Customer>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new Customer Create(DalCustomer entity)
        {
            var res = context.Set<Customer>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
