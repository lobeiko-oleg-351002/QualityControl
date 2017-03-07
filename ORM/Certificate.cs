namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Certificate")]
    public partial class Certificate : IOrmEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Certificate()
        {
            SelectedCertificate = new HashSet<SelectedCertificate>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string title { get; set; }

        [Column(TypeName = "date")]
        public DateTime checkDate { get; set; }

        public int? controlName_id { get; set; }

        public int? duration { get; set; }

        public int? employee_id { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SelectedCertificate> SelectedCertificate { get; set; }
    }
}
