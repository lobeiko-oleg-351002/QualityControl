using DAL.Entities;
using DAL.Mapping.Interfaces;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping
{
    public class CustomerMapper : ICustomerMapper
    {
        public DalCustomer MapToDal(Customer entity)
        {
            return new DalCustomer
            {
                Id = entity.id,
                Address = entity.address,
                Contract = entity.address,
                ContractBeginDate = entity.contractBeginDate,
                ContractEndDate = entity.contractEndDate,
                Organization = entity.organization,
                Phone = entity.phone
            };
        }

        public Customer MapToOrm(DalCustomer entity)
        {
            return new Customer
            {
                id = entity.Id,
                address = entity.Address,
                contract = entity.Address,
                contractBeginDate = entity.ContractBeginDate,
                contractEndDate = entity.ContractEndDate,
                organization = entity.Organization,
                phone = entity.Phone
            };
        }
    }
}
