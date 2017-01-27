using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UIL.Entities.Interface
{
    [DataContract]
    public class UilCertificate : IUilEntity
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public DateTime CheckDate { get; set; }

        [DataMember]
        public UilControlName ControlName { get; set; }

        [DataMember]
        public int? Duration { get; set; }

        [DataMember]
        public UilEmployee Employee { get; set; }
    }
}
