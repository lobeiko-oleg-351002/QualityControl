namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SelectedControlMethodDocumentation")]
    public partial class SelectedControlMethodDocumentation : IOrmEntity
    {
        public int id { get; set; }

        public int? controlMethodDocumentation_id { get; set; }

        public int? controlMethodDocumentationLib_id { get; set; }

        public virtual ControlMethodDocumentation ControlMethodDocumentation { get; set; }

        public virtual ControlMethodDocumentationLib ControlMethodDocumentationLib { get; set; }
    }
}
