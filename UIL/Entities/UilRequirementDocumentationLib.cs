using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities.Interface;

namespace UIL.Entities
{
    public class UilRequirementDocumentationLib : IUilEntity
    {
        public int Id { get; set; }

        public List<UilSelectedRequirementDocumentation> SelectedRequirementDocumentation { get; set; }

        public UilRequirementDocumentationLib()
        {
            SelectedRequirementDocumentation = new List<UilSelectedRequirementDocumentation>();
        }
    }
}
