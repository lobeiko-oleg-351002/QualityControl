using BLL.Entities;
using BLL.Mapping.Interfaces;
using BLL.Services;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class SelectedRequirementDocumentationMapper : ISelectedRequirementDocumentationMapper
    {
        IUnitOfWork uow;
        public SelectedRequirementDocumentationMapper(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public DalSelectedRequirementDocumentation MapToDal(BllSelectedRequirementDocumentation entity)
        {
            DalSelectedRequirementDocumentation dalEntity = new DalSelectedRequirementDocumentation
            {
                Id = entity.Id,
                RequirementDocumentation_id = entity.RequirementDocumentation.Id,
            };

            return dalEntity;
        }

        public BllSelectedRequirementDocumentation MapToBll(DalSelectedRequirementDocumentation entity)
        {
            RequirementDocumentationService requirementDocumentationService = new RequirementDocumentationService(uow);
            var bllRequirementDocumentation = entity.RequirementDocumentation_id != null ? requirementDocumentationService.Get((int)entity.RequirementDocumentation_id) : null;

            BllSelectedRequirementDocumentation bllEntity = new BllSelectedRequirementDocumentation
            {
                Id = entity.Id,
                RequirementDocumentation = bllRequirementDocumentation
            };

            return bllEntity;
        }
    }
}
