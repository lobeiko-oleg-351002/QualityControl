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
    public class ControlMethodDocumentationLibMapper : IControlMethodDocumentationLibMapper
    {
        IUnitOfWork uow;
        public ControlMethodDocumentationLibMapper(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public DalControlMethodDocumentationLib MapToDal(BllControlMethodDocumentationLib entity)
        {
            return new DalControlMethodDocumentationLib
            {
                Id = entity.Id
            };
        }

        public BllControlMethodDocumentationLib MapToBll(DalControlMethodDocumentationLib entity)
        {
            BllControlMethodDocumentationLib bllEntity = new BllControlMethodDocumentationLib
            {
                Id = entity.Id,
            };

            ISelectedControlMethodDocumentationMapper selectedControlMethodDocumentationMapper = new SelectedControlMethodDocumentationMapper(uow);

            foreach (var ControlMethodDocumentation in uow.SelectedControlMethodDocumentations.GetControlMethodDocumentationsByLibId(bllEntity.Id))
            {
                var bllSelectedControlMethodDocumentation = selectedControlMethodDocumentationMapper.MapToBll(ControlMethodDocumentation);
                bllEntity.SelectedControlMethodDocumentation.Add(bllSelectedControlMethodDocumentation);
            }
            return bllEntity;
        }
    }
}
