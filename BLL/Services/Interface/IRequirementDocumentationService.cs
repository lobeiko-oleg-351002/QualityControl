using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IRequirementDocumentationService : IService<BllRequirementDocumentation>
    {
        BllRequirementDocumentation GetRequirementDocumentationByName(string name);
    }
}
