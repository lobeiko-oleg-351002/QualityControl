using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DalSelectedControlMethodDocumentation : IDalEntity
    {
        public int Id { get; set; }

        public int? ControlMethodDocumentation_id { get; set; }

        public int? ControlMethodDocumentationLib_id { get; set; }
    }
}
