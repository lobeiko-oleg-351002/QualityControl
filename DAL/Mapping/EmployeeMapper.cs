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
    public class EmployeeMapper : IEmployeeMapper
    {
        public DalEmployee MapToDal(Employee entity)
        {
            return new DalEmployee
            {
                Id = entity.id,
                Fathername = entity.fathername,
                Function = entity.function,
                KnowledgeCheckDate = entity.knowledgeCheckDate,
                MedicalCheckDate = entity.medicalCheckDate,
                Name = entity.name,
                Sirname = entity.sirname
            };
        }

        public Employee MapToOrm(DalEmployee entity)
        {
            return new Employee
            {
                id = entity.Id,
                fathername = entity.Fathername,
                function = entity.Function,
                knowledgeCheckDate = entity.KnowledgeCheckDate,
                medicalCheckDate = entity.MedicalCheckDate,
                name = entity.Name,
                sirname = entity.Sirname
            };
        }
    }
}
