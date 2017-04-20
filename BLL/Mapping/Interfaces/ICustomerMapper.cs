using BLL.Entities;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping.Interfaces
{
    public interface ICustomerMapper
    {
        DalCustomer MapToDal(BllCustomer entity);
        BllCustomer MapToBll(DalCustomer entity);
    }
}
