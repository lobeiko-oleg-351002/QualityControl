using BLL.Entities;
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
    public class SelectedEmployeeService : Service<BllSelectedEmployee, DalSelectedEmployee>, ISelectedEmployeeService
    {
        private readonly IUnitOfWork uow;

        public SelectedEmployeeService(IUnitOfWork uow) : base(uow, uow.SelectedEmployees)
        {
            this.uow = uow;
        }
    }
}
