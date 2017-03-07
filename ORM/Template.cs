namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Template")]
    public partial class Template : IOrmEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Template()
        {
            Component = new HashSet<Component>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public int? material_id { get; set; }

        public int? weldJoint_id { get; set; }

        [StringLength(200)]
        public string description { get; set; }

        public int? equipmentLib_id { get; set; }

        public int? imageLib_id { get; set; }

        public int? controlNameLib_id { get; set; }

        public int? requirementDocumentationLib_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Component> Component { get; set; }

        public virtual ControlNameLib ControlNameLib { get; set; }

        public virtual EquipmentLib EquipmentLib { get; set; }

        public virtual ImageLib ImageLib { get; set; }

        public virtual Material Material { get; set; }

        public virtual RequirementDocumentationLib RequirementDocumentationLib { get; set; }

        public virtual WeldJoint WeldJoint { get; set; }
    }
}
