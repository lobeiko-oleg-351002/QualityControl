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
    public class RequirementDocumentationLibMapper : IRequirementDocumentationLibMapper
    {
        IUnitOfWork uow;
        public RequirementDocumentationLibMapper(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public DalRequirementDocumentationLib MapToDal(BllRequirementDocumentationLib entity)
        {
            return new DalRequirementDocumentationLib
            {
                Id = entity.Id
            };
        }

        public BllRequirementDocumentationLib MapToBll(DalRequirementDocumentationLib entity)
        {
            BllRequirementDocumentationLib bllEntity = new BllRequirementDocumentationLib
            {
                Id = entity.Id
            };

            ISelectedRequirementDocumentationMapper selectedRequirementDocumentationMapper = new SelectedRequirementDocumentationMapper(uow);

            foreach (var RequirementDocumentation in uow.SelectedRequirementDocumentations.GetRequirementDocumentationsByLibId(bllEntity.Id))
            {
                var bllSelectedRequirementDocumentation = selectedRequirementDocumentationMapper.MapToBll(RequirementDocumentation);
                bllEntity.SelectedRequirementDocumentation.Add(bllSelectedRequirementDocumentation);
            }
            return bllEntity;
        }
    }
}
