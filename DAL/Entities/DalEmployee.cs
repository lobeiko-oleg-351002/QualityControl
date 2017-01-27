using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DalEmployee : IDalEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Sirname { get; set; }

        public string Fathername { get; set; }

        public string Function { get; set; }

        //public int? Certificate_lib_id { get; set; }

        public DateTime? MedicalCheckDate { get; set; }

        public DateTime? KnowledgeCheckDate { get; set; }

        public int? Creator_id { get; set; }
    }
}
