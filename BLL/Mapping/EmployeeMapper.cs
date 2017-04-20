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
    public class EmployeeMapper : IEmployeeMapper
    {

        public EmployeeMapper()
        {

        }

        public DalEmployee MapToDal(BllEmployee entity)
        {
            DalEmployee dalEntity = new DalEmployee
            {
                Id = entity.Id,
                Fathername = entity.Fathername,
                Function = entity.Function,
                KnowledgeCheckDate = entity.KnowledgeCheckDate,
                MedicalCheckDate = entity.MedicalCheckDate,
                Name = entity.Name,
                Sirname = entity.Sirname
            };

            return dalEntity;
        }



        public BllEmployee MapToBll(DalEmployee entity)
        {
            BllEmployee bllEntity = new BllEmployee
            {
                Id = entity.Id,
                Fathername = entity.Fathername,
                Function = entity.Function,
                KnowledgeCheckDate = entity.KnowledgeCheckDate,
                MedicalCheckDate = entity.MedicalCheckDate,
                Name = entity.Name,
                Sirname = entity.Sirname
            };

            return bllEntity;
        }
    }
}
