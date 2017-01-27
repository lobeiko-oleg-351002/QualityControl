using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DalSelectedRequirementDocumentation : IDalEntity
    {
        public int Id { get; set; }

        public int? RequirementDocumentation_id { get; set; }

        public int? RequirementDocumentationLib_id { get; set; }
    }
}
