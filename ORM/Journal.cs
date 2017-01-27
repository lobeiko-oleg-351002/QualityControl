namespace ORM
{
    using Interface;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Journal")]
    public partial class Journal : IOrmEntity
    {
        public int id { get; set; }

        public DateTime? request_date { get; set; }

        public DateTime? control_date { get; set; }

        public int? request_number { get; set; }

        public int? industrial_object_id { get; set; }

        public int? amount { get; set; }

        [StringLength(50)]
        public string size { get; set; }

        public int? material_id { get; set; }

        public int? weld_joint_id { get; set; }

        [StringLength(50)]
        public string welding_type { get; set; }

        public int? component_id { get; set; }

        public int? customer_id { get; set; }

        [StringLength(50)]
        public string contract { get; set; }

        [StringLength(200)]
        public string description { get; set; }

        public int? controlMethodsLib_id { get; set; }

        public virtual Component Component { get; set; }

        public virtual ControlMethodsLib ControlMethodsLib { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual IndustrialObject IndustrialObject { get; set; }

        public virtual WeldJoint WeldJoint { get; set; }
    }
}
