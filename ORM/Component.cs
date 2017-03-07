namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Component")]
    public partial class Component : IOrmEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Component()
        {
            Journal = new HashSet<Journal>();
            SelectedComponent = new HashSet<SelectedComponent>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public int? template_id { get; set; }

        [StringLength(50)]
        public string pressmark { get; set; }

        public virtual Template Template { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Journal> Journal { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SelectedComponent> SelectedComponent { get; set; }
    }
}
