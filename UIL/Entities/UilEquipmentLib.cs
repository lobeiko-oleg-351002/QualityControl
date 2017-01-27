using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities.Interface;

namespace UIL.Entities
{
    public class UilEquipmentLib : IUilEntity
    {
        public int Id { get; set; }

        public List<UilSelectedEquipment> SelectedEquipment { get; set; }

        public UilEquipmentLib()
        {
            SelectedEquipment = new List<UilSelectedEquipment>();
        }
    }
}
