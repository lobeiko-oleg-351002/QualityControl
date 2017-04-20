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
    public class RequirementDocumentationLibMapper : IRequirementDocumentationLibMapper
    {
        public DalRequirementDocumentationLib MapToDal(RequirementDocumentationLib entity)
        {
            return new DalRequirementDocumentationLib
            {
                Id = entity.id,
            };
        }

        public RequirementDocumentationLib MapToOrm(DalRequirementDocumentationLib entity)
        {
            return new RequirementDocumentationLib
            {
                id = entity.Id
            };
        }
    }
}
