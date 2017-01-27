using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllEquipmentLib : IBllEntity
    {
        public int Id { get; set; }

        public List<BllSelectedEquipment> SelectedEquipment { get; set; }

        public BllEquipmentLib()
        {
            SelectedEquipment = new List<BllSelectedEquipment>();
        }
    }
}
