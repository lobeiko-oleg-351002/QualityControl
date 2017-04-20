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
    public class EmployeeRepository : Repository<DalEmployee,Employee>, IEmployeeRepository
    {
        private readonly ServiceDB context;
        public EmployeeRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<DalEmployee> GetEmployeesByFatherName(string name)
        {
            EmployeeMapper mapper = new EmployeeMapper();
            var elements = context.Employees.Select(entity => entity.fathername == name);
            var retElemets = new List<DalEmployee>();
            foreach (var element in elements)
            {
               // retElemets.Add(mapper.MapToDal(element));
            }
            return retElemets;
        }

        public IEnumerable<DalEmployee> GetEmployeesByFunction(string function)
        {
            EmployeeMapper mapper = new EmployeeMapper();
            var elements = context.Employees.Select(entity => entity.function == function);
            var retElemets = new List<DalEmployee>();
            foreach (var element in elements)
            {
               // retElemets.Add(Mapper.Map<DalEmployee>(element));
            }
            return retElemets;
        }

        public IEnumerable<DalEmployee> GetEmployeesByName(string name)
        {
            EmployeeMapper mapper = new EmployeeMapper();
            var elements = context.Employees.Select(entity => entity.name == name);
            var retElemets = new List<DalEmployee>();
            foreach (var element in elements)
            {
              //  retElemets.Add(Mapper.Map<DalEmployee>(element));
            }
            return retElemets;
        }

        public IEnumerable<DalEmployee> GetEmployeesBySirname(string name)
        {
            EmployeeMapper mapper = new EmployeeMapper();
            var elements = context.Employees.Select(entity => entity.sirname == name);
            var retElemets = new List<DalEmployee>();
            foreach (var element in elements)
            {
               // retElemets.Add(Mapper.Map<DalEmployee>(element));
            }
            return retElemets;
        }

        EmployeeMapper mapper = new EmployeeMapper();

        public new void Delete(DalEmployee entity)
        {
            var ormEntity = context.Set<Employee>().Single(Employee => Employee.id == entity.Id);
            context.Set<Employee>().Remove(ormEntity);
        }

        public new DalEmployee Get(int id)
        {
            var ormEntity = context.Set<Employee>().FirstOrDefault(Employee => Employee.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalEmployee> GetAll()
        {
            var elements = context.Set<Employee>().Select(Employee => Employee);
            var retElemets = new List<DalEmployee>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalEmployee entity)
        {
            var ormEntity = context.Set<Employee>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new Employee Create(DalEmployee entity)
        {
            var res = context.Set<Employee>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
