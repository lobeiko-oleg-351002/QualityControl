namespace ORM
{
    using Interface;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WeldJoint")]
    public partial class WeldJoint : IOrmEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WeldJoint()
        {
            Journal = new HashSet<Journal>();
            Template = new HashSet<Template>();
        }

        public int id { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        public string description { get; set; }

        [Column(TypeName = "image")]
        public byte[] image { get; set; }

        public int? creator_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Journal> Journal { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Template> Template { get; set; }

        public virtual User User { get; set; }
    }
}
