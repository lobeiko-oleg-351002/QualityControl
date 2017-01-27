using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities.Interface;

namespace UIL.Entities
{
    public class UilTemplate : IUilEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public UilMaterial Material { get; set; }

        public UilWeldJoint WeldJoint { get; set; }

        public string Description { get; set; }

        public UilEquipmentLib EquipmentLib { get; set; }

        public UilImageLib ImageLib { get; set; }

        public UilControlNameLib ControlNameLib { get; set; }

        public UilRequirementDocumentationLib RequirementDocumentationLib { get; set; }
    }
}
