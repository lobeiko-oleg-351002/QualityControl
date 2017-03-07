namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Result")]
    public partial class Result : IOrmEntity
    {
        public int id { get; set; }

        public int? number { get; set; }

        [StringLength(100)]
        public string welder { get; set; }

        [StringLength(50)]
        public string mark { get; set; }

        [StringLength(200)]
        public string defectDescription { get; set; }

        [StringLength(100)]
        public string norm { get; set; }

        [StringLength(50)]
        public string quality { get; set; }

        public int? resultLib_id { get; set; }

        public virtual ResultLib ResultLib { get; set; }
    }
}
