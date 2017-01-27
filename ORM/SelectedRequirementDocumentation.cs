namespace ORM
{
    using Interface;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SelectedRequirementDocumentation")]
    public partial class SelectedRequirementDocumentation : IOrmEntity
    {
        public int id { get; set; }

        public int? requirementDocumentation_id { get; set; }

        public int? requirementDocumentationLib_id { get; set; }

        public virtual RequirementDocumentation RequirementDocumentation { get; set; }

        public virtual RequirementDocumentationLib RequirementDocumentationLib { get; set; }
    }
}
