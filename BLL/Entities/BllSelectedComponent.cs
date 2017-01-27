using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllSelectedComponent : IBllEntity
    {
        public int Id { get; set; }

        public BllComponent Component { get; set; }

    }
}
