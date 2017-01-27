using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DalControl : IDalEntity
    {
        public int Id { get; set; }

        public bool? Is_controlled { get; set; }

        public int? Number { get; set; }

        public float? Light { get; set; }

        public string Additionally { get; set; }

        public int? Requirement_documentation_lib_id { get; set; }

        public int? Control_method_documentation_lib_id { get; set; }

        public int? Image_lib_id { get; set; }

        public int? ResultLib_id { get; set; }

        public int? EmployeeLib_id { get; set; }

        public int? Control_name_id { get; set; }

        public int? ControlMethodsLib_id { get; set; }

        public int? EquipmentLib_id { get; set; }

        public int? ProtocolNumber { get; set; }
    }
}
