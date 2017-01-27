using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllSelectedControlName : IBllEntity
    {
        public int Id { get; set; }

        public BllControlName ControlName { get; set; }
    }
}
