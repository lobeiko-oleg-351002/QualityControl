namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Control")]
    public partial class Control : IOrmEntity
    {
        public int id { get; set; }

        public bool? isControlled { get; set; }

        public int? number { get; set; }

        public float? light { get; set; }

        [StringLength(200)]
        public string additionally { get; set; }

        public int? requirementDocumentationLib_id { get; set; }

        public int? controlMethodDocumentationLib_id { get; set; }

        public int? imageLib_id { get; set; }

        public int? employeeLib_id { get; set; }

        public int? equipmentLib_id { get; set; }

        public int? controlName_id { get; set; }

        public int? controlMethodsLib_id { get; set; }

        public int? resultLib_id { get; set; }

        public int? protocolNumber { get; set; }

        public virtual ControlMethodDocumentationLib ControlMethodDocumentationLib { get; set; }

        public virtual ControlMethodsLib ControlMethodsLib { get; set; }

        public virtual ControlName ControlName { get; set; }

        public virtual EmployeeLib EmployeeLib { get; set; }

        public virtual EquipmentLib EquipmentLib { get; set; }

        public virtual ImageLib ImageLib { get; set; }

        public virtual RequirementDocumentationLib RequirementDocumentationLib { get; set; }

        public virtual ResultLib ResultLib { get; set; }
    }
}
