using BLL.Entities;
using BLL.Mapping.Interfaces;
using BLL.Services;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class CustomerMapper : ICustomerMapper
    {

        public CustomerMapper()
        {

        }

        public DalCustomer MapToDal(BllCustomer entity)
        {
            DalCustomer dalEntity = new DalCustomer
            {
                Id = entity.Id,
                Address = entity.Address,
                Contract = entity.Contract,
                ContractBeginDate = entity.ContractBeginDate,
                ContractEndDate = entity.ContractEndDate,
                Organization = entity.Organization,
                Phone = entity.Phone
            };

            return dalEntity;
        }


        public BllCustomer MapToBll(DalCustomer entity)
        {
            BllCustomer bllEntity = new BllCustomer
            {
                Id = entity.Id,
                Address = entity.Address,
                Contract = entity.Contract,
                ContractBeginDate = entity.ContractBeginDate,
                ContractEndDate = entity.ContractEndDate,
                Organization = entity.Organization,
                Phone = entity.Phone
            };

            return bllEntity;
        }
    }
}
