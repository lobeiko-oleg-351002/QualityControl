namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SelectedEquipment")]
    public partial class SelectedEquipment : IOrmEntity
    {
        public int id { get; set; }

        public int? equipment_id { get; set; }

        public int? equipmentLib_id { get; set; }

        public virtual Equipment Equipment { get; set; }

        public virtual EquipmentLib EquipmentLib { get; set; }
    }
}
