using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllSelectedRequirementDocumentation : IBllEntity
    {
        public int Id { get; set; }

        public BllRequirementDocumentation RequirementDocumentation { get; set; }
    }
}
