using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IRequirementDocumentationLibService : IService<BllRequirementDocumentationLib>
    {
        new BllRequirementDocumentationLib Create(BllRequirementDocumentationLib entity);
        new BllRequirementDocumentationLib Update(BllRequirementDocumentationLib entity);
    }
}
