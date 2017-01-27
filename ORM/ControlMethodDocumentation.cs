namespace ORM
{
    using Interface;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ControlMethodDocumentation")]
    public partial class ControlMethodDocumentation : IOrmEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ControlMethodDocumentation()
        {
            SelectedControlMethodDocumentation = new HashSet<SelectedControlMethodDocumentation>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public int? creator_id { get; set; }

        [StringLength(50)]
        public string pressmark { get; set; }

        public int? controlName_id { get; set; }

        public virtual ControlName ControlName { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SelectedControlMethodDocumentation> SelectedControlMethodDocumentation { get; set; }
    }
}
