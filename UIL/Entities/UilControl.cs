using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities.Interface;

namespace UIL.Entities
{
    public class UilControl : IUilEntity
    {
        public int Id { get; set; }

        public bool? IsControlled { get; set; }

        public int? Number { get; set; }

        public float? Light { get; set; }

        public string Additionally { get; set; }

        public UilRequirementDocumentationLib RequirementDocumentationLib { get; set; }

        public UilControlMethodDocumentationLib ControlMethodDocumentationLib { get; set; }

        public UilImageLib ImageLib { get; set; }

        public UilResultLib ResultLib { get; set; }

        public UilEmployeeLib EmployeeLib { get; set; }

        public UilEquipmentLib EquipmentLib { get; set; }

        public UilControlName ControlName { get; set; }

        public int? ProtocolNumber { get; set; }

    }
}
