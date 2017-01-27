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
    public class UilEquipment : IUilEntity
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public int? FactoryNumber { get; set; }

        [DataMember]
        public DateTime? CheckDate { get; set; }

        [DataMember]
        public Byte[] IsChecked { get; set; }

        [DataMember]
        public DateTime? TechnicalCheckDate { get; set; }

        [DataMember]
        public DateTime? NextTechnicalCheckDate { get; set; }

        [DataMember]
        public string Pressmark { get; set; }

        [DataMember]
        public string NumberOfTechnicalCheck { get; set; }
    }
}
