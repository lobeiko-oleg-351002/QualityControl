using BLL.Entities;
using BLL.Mapping.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class RequirementDocumentationMapper : IRequirementDocumentationMapper
    {
        public RequirementDocumentationMapper()
        {

        }

        public DalRequirementDocumentation MapToDal(BllRequirementDocumentation entity)
        {
            DalRequirementDocumentation dalEntity = new DalRequirementDocumentation
            {
                Id = entity.Id,
                Name = entity.Name,
                ObjectGroup = entity.ObjectGroup,
                Pressmark = entity.Pressmark
            };

            return dalEntity;
        }

        public BllRequirementDocumentation MapToBll(DalRequirementDocumentation entity)
        {
            BllRequirementDocumentation bllEntity = new BllRequirementDocumentation
            {
                Id = entity.Id,
                Name = entity.Name,
                ObjectGroup = entity.ObjectGroup,
                Pressmark = entity.Pressmark
            };

            return bllEntity;
        }
    }
}
