using BLL.Entities;
using BLL.Mapping;
using BLL.Mapping.Interfaces;
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
            bllMapper = new SelectedEmployeeMapper(uow);
        }
        ISelectedEmployeeMapper bllMapper;
        public override void Create(BllSelectedEmployee entity)
        {
            uow.SelectedEmployees.Create(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Update(BllSelectedEmployee entity)
        {
            uow.SelectedEmployees.Update(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllSelectedEmployee entity)
        {
            uow.SelectedEmployees.Delete(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override IEnumerable<BllSelectedEmployee> GetAll()
        {
            var elements = uow.SelectedEmployees.GetAll();
            var retElemets = new List<BllSelectedEmployee>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }

        public override BllSelectedEmployee Get(int id)
        {
            return bllMapper.MapToBll(uow.SelectedEmployees.Get(id));
        }
    }
}
