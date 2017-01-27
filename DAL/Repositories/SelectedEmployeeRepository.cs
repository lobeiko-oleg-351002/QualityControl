using AutoMapper;
using DAL.Entities;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SelectedEmployeeRepository : Repository<DalSelectedEmployee, SelectedEmployee>, ISelectedEmployeeRepository
    {
        private readonly ServiceDB context;
        public SelectedEmployeeRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<DalSelectedEmployee> GetEmployeesByLibId(int id)
        {
            Mapper.CreateMap<SelectedEmployee, DalSelectedEmployee>();
            var elements = context.Set<SelectedEmployee>().Where(entity => entity.employeeLib_id == id);
            var retElemets = new List<DalSelectedEmployee>();
            foreach (var element in elements)
            {
                Mapper.CreateMap<SelectedEmployee, DalSelectedEmployee>();
                retElemets.Add(Mapper.Map<DalSelectedEmployee>(element));
            }
            return retElemets;
        }

        public new SelectedEmployee Create(DalSelectedEmployee entity)
        {
            Mapper.CreateMap<DalSelectedEmployee, SelectedEmployee>();
            var ormEntity = Mapper.Map<SelectedEmployee>(entity);
            ormEntity.EmployeeLib = context.EmployeeLibs.FirstOrDefault(e => e.id == ormEntity.employeeLib_id);
            return context.Set<SelectedEmployee>().Add(Mapper.Map<SelectedEmployee>(entity));
        }
    }
}
