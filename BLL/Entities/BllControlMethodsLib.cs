using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllControlMethodsLib : IBllEntity
    {
        public int Id { get; set; }

        public List<BllControl> Control { get; set; }

        public BllControlMethodsLib()
        {
            Control = new List<BllControl>();
        }
    }
}
