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
    public class RequirementDocumentationMapper : IRequirementDocumentationMapper
    {
        public DalRequirementDocumentation MapToDal(RequirementDocumentation entity)
        {
            return new DalRequirementDocumentation
            {
                Id = entity.id,
                Name = entity.name,
                ObjectGroup = entity.objectGroup,
                Pressmark = entity.pressmark
            };
        }

        public RequirementDocumentation MapToOrm(DalRequirementDocumentation entity)
        {
            return new RequirementDocumentation
            {
                id = entity.Id,
                name = entity.Name,
                objectGroup = entity.ObjectGroup,
                pressmark = entity.Pressmark
            };
        }
    }
}
