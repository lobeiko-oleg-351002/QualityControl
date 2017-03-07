using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities.Interface;

namespace UIL.Entities
{
    public class UilJournal : IUilEntity
    {
        public int Id { get; set; }

        public DateTime? Request_date { get; set; }

        public DateTime? Control_date { get; set; }

        public int? Request_number { get; set; }

        public UilIndustrialObject IndustrialObject { get; set; }

        public int? Amount { get; set; }

        public string Size { get; set; }

        public UilMaterial Material { get; set; }

        public UilWeldJoint WeldJoint { get; set; }

        public string WeldingType { get; set; }

        public UilControlMethodsLib ControlMethodsLib { get; set; }

        public UilComponent Component { get; set; }

        public UilCustomer Customer { get; set; }

        public string Contract { get; set; }

        public string Description { get; set; }

        public UilUser UserOwner { get; set; }

        public string UserModifierLogin { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
