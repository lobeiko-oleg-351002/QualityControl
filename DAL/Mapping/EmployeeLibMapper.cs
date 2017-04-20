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
    public class EmployeeLibMapper : IEmployeeLibMapper
    {
        public DalEmployeeLib MapToDal(EmployeeLib entity)
        {
            return new DalEmployeeLib
            {
                Id = entity.id,
            };
        }

        public EmployeeLib MapToOrm(DalEmployeeLib entity)
        {
            return new EmployeeLib
            {
                id = entity.Id
            };
        }
    }
}
