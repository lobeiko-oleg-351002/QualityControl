using AutoMapper;
using DAL.Entities;
using DAL.Mapping;
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
            var elements = context.Set<SelectedEmployee>().Where(entity => entity.employeeLib_id == id);
            var retElemets = new List<DalSelectedEmployee>();
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapToDal(element));
            }
            return retElemets;
        }

        public new SelectedEmployee Create(DalSelectedEmployee entity)
        {
            var ormEntity = mapper.MapToOrm(entity);
            ormEntity.EmployeeLib = context.EmployeeLibs.FirstOrDefault(e => e.id == ormEntity.employeeLib_id);
            return context.Set<SelectedEmployee>().Add(ormEntity);
        }

        SelectedEmployeeMapper mapper = new SelectedEmployeeMapper();

        public new void Delete(DalSelectedEmployee entity)
        {
            var ormEntity = context.Set<SelectedEmployee>().Single(SelectedEmployee => SelectedEmployee.id == entity.Id);
            context.Set<SelectedEmployee>().Remove(ormEntity);
        }

        public new DalSelectedEmployee Get(int id)
        {
            var ormEntity = context.Set<SelectedEmployee>().FirstOrDefault(SelectedEmployee => SelectedEmployee.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalSelectedEmployee> GetAll()
        {
            var elements = context.Set<SelectedEmployee>().Select(SelectedEmployee => SelectedEmployee);
            var retElemets = new List<DalSelectedEmployee>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalSelectedEmployee entity)
        {
            var ormEntity = context.Set<SelectedEmployee>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

    }
}
