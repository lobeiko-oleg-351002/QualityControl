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

        public DateTime? RequestDate { get; set; }

        public DateTime? ControlDate { get; set; }

        public int? RequestNumber { get; set; }

        public BllIndustrialObject IndustrialObject { get; set; }

        public int? Amount { get; set; }

        public string Size { get; set; }

        public BllMaterial Material { get; set; }

        public BllWeldJoint WeldJoint { get; set; }

        public string WeldingType { get; set; }

        public BllControlMethodsLib ControlMethodsLib { get; set; }

        public BllComponent Component { get; set; }

        public BllCustomer Customer { get; set; }

        public string Contract { get; set; }

        public string Description { get; set; }

        public BllUser UserOwner { get; set; }

        public string UserModifierLogin { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
