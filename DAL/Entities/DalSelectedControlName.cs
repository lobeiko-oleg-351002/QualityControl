using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DalSelectedControlName : IDalEntity
    {
        public int Id { get; set; }

        public int? ControlName_id { get; set; }

        public int? ControlNameLib_id { get; set; }
    }
}
