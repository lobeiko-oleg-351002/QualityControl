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
    public class UilCustomer : IUilEntity
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Organization { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string Contract { get; set; }

        [DataMember]
        public DateTime? ContractBeginDate { get; set; }

        [DataMember]
        public DateTime? ContractEndDate { get; set; }
    }
}
