using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities.Interface;

namespace UIL.Entities
{
    [DataContract]
    public class UilEmployee : IUilEntity
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Sirname { get; set; }

        [DataMember]
        public string Fathername { get; set; }

        [DataMember]
        public string Function { get; set; }

        //[DataMember]
        //public UilCertificateLib CertificateLib { get; set; }

        [DataMember]
        public DateTime? MedicalCheckDate { get; set; }

        [DataMember]
        public DateTime? KnowledgeCheckDate { get; set; }

        [DataMember]
        public int? Creator_id { get; set; }
    }
}
