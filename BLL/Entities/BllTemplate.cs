using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllTemplate : IBllEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public BllMaterial Material { get; set; }

        public int? Creator_id { get; set; }

        public BllWeldJoint WeldJoint { get; set; }

        public string Description { get; set; }

        public BllEquipmentLib EquipmentLib { get; set; }

        public BllImageLib ImageLib { get; set; }

        public BllControlNameLib ControlNameLib { get; set; }

        public BllRequirementDocumentationLib RequirementDocumentationLib { get; set; }
    }
}
