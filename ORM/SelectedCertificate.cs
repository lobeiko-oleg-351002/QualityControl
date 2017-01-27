namespace ORM
{
    using Interface;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SelectedCertificate")]
    public partial class SelectedCertificate : IOrmEntity
    {
        public int id { get; set; }

        public int? certificate_id { get; set; }

        public int? certificate_lib_id { get; set; }

        public virtual Certificate Certificate { get; set; }

        public virtual CertificateLib CertificateLib { get; set; }
    }
}
