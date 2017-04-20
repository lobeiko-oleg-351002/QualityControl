using DAL.Entities;
using DAL.Mapping.Interfaces;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping
{
    public class SelectedRequirementDocumentationMapper : ISelectedRequirementDocumentationMapper
    {
        public DalSelectedRequirementDocumentation MapToDal(SelectedRequirementDocumentation entity)
        {
            return new DalSelectedRequirementDocumentation
            {
                Id = entity.id,
                RequirementDocumentationLib_id = entity.requirementDocumentationLib_id,
                RequirementDocumentation_id = entity.requirementDocumentation_id
            };
        }

        public SelectedRequirementDocumentation MapToOrm(DalSelectedRequirementDocumentation entity)
        {
            return new SelectedRequirementDocumentation
            {
                id = entity.Id,
                requirementDocumentation_id = entity.RequirementDocumentation_id,
                requirementDocumentationLib_id = entity.RequirementDocumentationLib_id
            };
        }
    }
}
