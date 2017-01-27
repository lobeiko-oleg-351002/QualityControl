using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllComponentLib : IBllEntity
    {
        public int Id { get; set; }

        public List<BllSelectedComponent> SelectedComponent { get; set; }

        public BllComponentLib()
        {
            SelectedComponent = new List<BllSelectedComponent>();
        }
    }
}
