namespace ORM
{
    using Interface;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee : IOrmEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            Certificate = new HashSet<Certificate>();
            SelectedEmployee = new HashSet<SelectedEmployee>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [Required]
        [StringLength(50)]
        public string sirname { get; set; }

        [Required]
        [StringLength(50)]
        public string fathername { get; set; }

        [Required]
        [StringLength(50)]
        public string function { get; set; }

        [Column(TypeName = "date")]
        public DateTime? medicalCheckDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? knowledgeCheckDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Certificate> Certificate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SelectedEmployee> SelectedEmployee { get; set; }
    }
}
