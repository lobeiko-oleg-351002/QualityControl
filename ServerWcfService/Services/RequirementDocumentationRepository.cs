using AutoMapper;
using BLL.Entities;
using BLL.Services.Interface;
using ServerWcfService.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities;

namespace ServerWcfService.Services
{
    public class RequirementDocumentationRepository : Repository<UilRequirementDocumentation, BllRequirementDocumentation>, IRequirementDocumentationRepository
    {
        private readonly IRequirementDocumentationService requirementDocumentationService;

        public RequirementDocumentationRepository() : base(UiUnitOfWork.Instance.RequirementDocumentations)
        {
            requirementDocumentationService = UiUnitOfWork.Instance.RequirementDocumentations;
        }

        public UilRequirementDocumentation GetRequirementDocumentationByName(string name)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllRequirementDocumentation, UilRequirementDocumentation>();
            });
            return Mapper.Map<UilRequirementDocumentation>(requirementDocumentationService.GetRequirementDocumentationByName(name));
        }
    }
}
