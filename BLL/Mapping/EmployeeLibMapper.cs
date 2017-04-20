using BLL.Entities;
using BLL.Mapping.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class EmployeeLibMapper : IEmployeeLibMapper
    {
        IUnitOfWork uow;
        public EmployeeLibMapper(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public DalEmployeeLib MapToDal(BllEmployeeLib entity)
        {
            return new DalEmployeeLib
            {
                Id = entity.Id
            };
        }

        public BllEmployeeLib MapToBll(DalEmployeeLib entity)
        {
            BllEmployeeLib bllEntity = new BllEmployeeLib
            {
                Id = entity.Id
            };

            ISelectedEmployeeMapper selectedEmployeeMapper = new SelectedEmployeeMapper(uow);

            foreach (var Employee in uow.SelectedEmployees.GetEmployeesByLibId(bllEntity.Id))
            {
                var bllSelectedEmployee = selectedEmployeeMapper.MapToBll(Employee);
                bllEntity.SelectedEmployee.Add(bllSelectedEmployee);
            }
            return bllEntity;
        }
    }
}
