using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllControl : IBllEntity
    {
        public int Id { get; set; }

        public bool? IsControlled { get; set; }

        public int? Number { get; set; }

        public float? Light { get; set; }

        public string Additionally { get; set; }

        public BllRequirementDocumentationLib RequirementDocumentationLib { get; set; }

        public BllControlMethodDocumentationLib ControlMethodDocumentationLib { get; set; }

        public BllImageLib ImageLib { get; set; }

        public BllResultLib ResultLib { get; set; }

        public BllEmployeeLib EmployeeLib { get; set; }

        public BllEquipmentLib EquipmentLib { get; set; }

        public BllControlName ControlName { get; set; }

        public int? ProtocolNumber { get; set; }

        //public BllControlMethodsLib ControlMethodsLib { get; set; }
    }
}
