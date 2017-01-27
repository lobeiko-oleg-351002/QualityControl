using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DalCustomer : IDalEntity
    {
        public int Id { get; set; }

        public string Organization { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Contract { get; set; }

        public int? Creator_id { get; set; }

        public DateTime? ContractBeginDate { get; set; }
        
        public DateTime? ContractEndDate { get; set; }
    }
}
