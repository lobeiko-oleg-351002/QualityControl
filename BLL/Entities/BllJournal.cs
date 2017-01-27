using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllJournal : IBllEntity
    {
        public int Id { get; set; }

        public DateTime? Request_date { get; set; }

        public DateTime? Control_date { get; set; }

        public int? Request_number { get; set; }

        public BllIndustrialObject IndustrialObject { get; set; }

        public int? Amount { get; set; }

        public string Size { get; set; }

        public BllMaterial Material { get; set; }

        public BllWeldJoint WeldJoint { get; set; }

        public string Welding_type { get; set; }

        public BllControlMethodsLib ControlMethodsLib { get; set; }

        public BllComponent Component { get; set; }

        public BllCustomer Customer { get; set; }

        public string Contract { get; set; }

        public string Description { get; set; }
    }
}
