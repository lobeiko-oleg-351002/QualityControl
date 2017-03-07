using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DalTemplate : IDalEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Material_id { get; set; }

        public int? Creator_id { get; set; }

        public int? WeldJoint_id { get; set; }

        public string Description { get; set; }

        public int? EquipmentLib_id { get; set; }

        public int? ImageLib_id { get; set; }

        public int? ControlNameLib_id { get; set; }

        public int? RequirementDocumentationLib_id { get; set; }
    }
}
