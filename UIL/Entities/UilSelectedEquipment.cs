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
    public class UilSelectedEquipment : IUilEntity
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public UilEquipment Equipment { get; set; }

    }
}
