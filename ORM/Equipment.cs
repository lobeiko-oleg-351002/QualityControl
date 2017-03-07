namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Equipment")]
    public partial class Equipment : IOrmEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Equipment()
        {
            SelectedEquipment = new HashSet<SelectedEquipment>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [StringLength(50)]
        public string type { get; set; }

        public int? factoryNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? checkDate { get; set; }

        public bool? isChecked { get; set; }

        [Column(TypeName = "date")]
        public DateTime? technicalCheckDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? nextTechnicalCheckDate { get; set; }

        [StringLength(50)]
        public string pressmark { get; set; }

        [StringLength(50)]
        public string numberOfTechnicalCheck { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SelectedEquipment> SelectedEquipment { get; set; }
    }
}
