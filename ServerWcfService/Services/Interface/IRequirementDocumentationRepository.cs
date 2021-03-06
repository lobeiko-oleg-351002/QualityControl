﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities;

namespace ServerWcfService.Services.Interface
{
    [ServiceContract]
    public interface IRequirementDocumentationRepository : IRepository<UilRequirementDocumentation>
    {
        [OperationContract]
        UilRequirementDocumentation GetRequirementDocumentationByName(string name);
    }
}
