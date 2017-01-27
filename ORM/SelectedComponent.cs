namespace ORM
{
    using Interface;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SelectedComponent")]
    public partial class SelectedComponent : IOrmEntity
    {
        public int id { get; set; }

        public int? component_id { get; set; }

        public int? component_lib_id { get; set; }

        public virtual Component Component { get; set; }

        public virtual ComponentLib ComponentLib { get; set; }
    }
}
