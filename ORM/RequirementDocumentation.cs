namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RequirementDocumentation")]
    public partial class RequirementDocumentation : IOrmEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RequirementDocumentation()
        {
            SelectedRequirementDocumentation = new HashSet<SelectedRequirementDocumentation>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [StringLength(50)]
        public string objectGroup { get; set; }

        [StringLength(50)]
        public string pressmark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SelectedRequirementDocumentation> SelectedRequirementDocumentation { get; set; }
    }
}
