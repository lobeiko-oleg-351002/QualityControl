using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DalSelectedEquipment : IDalEntity
    {
        public int Id { get; set; }

        public int? Equipment_id { get; set; }

        public int? EquipmentLib_id { get; set; }
    }
}
