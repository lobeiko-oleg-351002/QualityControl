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
    public class SelectedEmployeeMapper : ISelectedEmployeeMapper
    {
        public DalSelectedEmployee MapToDal(SelectedEmployee entity)
        {
            return new DalSelectedEmployee
            {
                Id = entity.id,
                EmployeeLib_id = entity.employeeLib_id,
                Employee_id = entity.employee_id
            };
        }

        public SelectedEmployee MapToOrm(DalSelectedEmployee entity)
        {
            return new SelectedEmployee
            {
                id = entity.Id,
                employeeLib_id = entity.EmployeeLib_id,
                employee_id = entity.Employee_id
            };
        }
    }
}
