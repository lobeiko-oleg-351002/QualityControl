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

        public int? Weld_Joint_id { get; set; }

        public string Description { get; set; }

        public int? Equipment_lib_id { get; set; }

        public int? Image_lib_id { get; set; }

        public int? Control_name_lib_id { get; set; }

        public int? RequirementDocumentation_lib_id { get; set; }
    }
}
