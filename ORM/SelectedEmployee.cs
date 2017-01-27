namespace ORM
{
    using Interface;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SelectedEmployee")]
    public partial class SelectedEmployee : IOrmEntity
    {
        public int id { get; set; }

        public int? employee_id { get; set; }

        public int? employeeLib_id { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual EmployeeLib EmployeeLib { get; set; }
    }
}
