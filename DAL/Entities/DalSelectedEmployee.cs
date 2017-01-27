using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DalSelectedEmployee : IDalEntity
    {
        public int Id { get; set; }

        public int? Employee_id { get; set; }

        public int? EmployeeLib_id { get; set; }
    }
}
