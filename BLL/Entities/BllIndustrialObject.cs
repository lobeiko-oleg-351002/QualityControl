using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllIndustrialObject : IBllEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Creator_id { get; set; }

        public BllComponentLib ComponentLib { get; set; }
    }
}
