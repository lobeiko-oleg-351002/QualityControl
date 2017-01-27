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
    public class EmployeeRepository : Repository<DalEmployee,Employee>, IEmployeeRepository
    {
        private readonly ServiceDB context;
        public EmployeeRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<DalEmployee> GetEmployeesByFatherName(string name)
        {
            Mapper.CreateMap<Employee, DalEmployee>();
            var elements = context.Employees.Select(entity => entity.fathername == name);
            var retElemets = new List<DalEmployee>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<DalEmployee>(element));
            }
            return retElemets;
        }

        public IEnumerable<DalEmployee> GetEmployeesByFunction(string function)
        {
            Mapper.CreateMap<Employee, DalEmployee>();
            var elements = context.Employees.Select(entity => entity.function == function);
            var retElemets = new List<DalEmployee>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<DalEmployee>(element));
            }
            return retElemets;
        }

        public IEnumerable<DalEmployee> GetEmployeesByName(string name)
        {
            Mapper.CreateMap<Employee, DalEmployee>();
            var elements = context.Employees.Select(entity => entity.name == name);
            var retElemets = new List<DalEmployee>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<DalEmployee>(element));
            }
            return retElemets;
        }

        public IEnumerable<DalEmployee> GetEmployeesBySirname(string name)
        {
            Mapper.CreateMap<Employee, DalEmployee>();
            var elements = context.Employees.Select(entity => entity.sirname == name);
            var retElemets = new List<DalEmployee>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<DalEmployee>(element));
            }
            return retElemets;
        }
    }
}
