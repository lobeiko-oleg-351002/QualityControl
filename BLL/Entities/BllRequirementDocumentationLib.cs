using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllRequirementDocumentationLib : IBllEntity
    {
        public int Id { get; set; }

        public List<BllSelectedRequirementDocumentation> SelectedRequirementDocumentation { get; set; }

        public BllRequirementDocumentationLib()
        {
            SelectedRequirementDocumentation = new List<BllSelectedRequirementDocumentation>();
        }
    }
}
