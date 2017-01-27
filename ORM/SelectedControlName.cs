namespace ORM
{
    using Interface;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SelectedControlName")]
    public partial class SelectedControlName : IOrmEntity
    {
        public int id { get; set; }

        public int? controlName_id { get; set; }

        public int? controlNameLib_id { get; set; }

        public virtual ControlName ControlName { get; set; }

        public virtual ControlNameLib ControlNameLib { get; set; }
    }
}
