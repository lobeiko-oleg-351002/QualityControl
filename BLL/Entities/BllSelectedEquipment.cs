using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllSelectedEquipment : IBllEntity
    {
        public int Id { get; set; }

        public BllEquipment Equipment { get; set; }
    }
}
