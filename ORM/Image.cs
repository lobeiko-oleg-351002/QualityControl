namespace ORM
{
    using Interface;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Image")]
    public partial class Image : IOrmEntity
    {
        public int id { get; set; }

        [Column("image", TypeName = "image")]
        public byte[] image { get; set; }

        public int? image_lib_Id { get; set; }

        public virtual ImageLib ImageLib { get; set; }
    }
}
