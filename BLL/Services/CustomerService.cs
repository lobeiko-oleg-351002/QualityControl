using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
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
        CustomerMapper bllMapper = new CustomerMapper();
        public CustomerService(IUnitOfWork uow) : base(uow, uow.Customers)
        {
            this.uow = uow;
        }

        public override void Create(BllCustomer entity)
        {
            uow.Customers.Create(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Update(BllCustomer entity)
        {
            uow.Customers.Update(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllCustomer entity)
        {
            uow.Customers.Delete(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override IEnumerable<BllCustomer> GetAll()
        {
            var elements = uow.Customers.GetAll();
            var retElemets = new List<BllCustomer>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }

        public override BllCustomer Get(int id)
        {
            return bllMapper.MapToBll(uow.Customers.Get(id));
        }

        public BllCustomer GetCustomerByAddress(string address)
        {
            return bllMapper.MapToBll(uow.Customers.GetCustomerByAddress(address));
        }
        public BllCustomer GetCustomerByContract(string contract)
        {
            return bllMapper.MapToBll(uow.Customers.GetCustomerByContract(contract));
        }

        public BllCustomer GetCustomerByOrganization(string organization)
        {
            return bllMapper.MapToBll(uow.Customers.GetCustomerByOrganization(organization));
        }

        public BllCustomer GetCustomerByPhone(string phone)
        {
            return bllMapper.MapToBll(uow.Customers.GetCustomerByPhone(phone));
        }
    }
}
