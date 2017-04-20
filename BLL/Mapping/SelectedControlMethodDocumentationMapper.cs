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
    public class SelectedControlMethodDocumentationMapper : ISelectedControlMethodDocumentationMapper
    {
        IUnitOfWork uow;
        public SelectedControlMethodDocumentationMapper(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public DalSelectedControlMethodDocumentation MapToDal(BllSelectedControlMethodDocumentation entity)
        {
            DalSelectedControlMethodDocumentation dalEntity = new DalSelectedControlMethodDocumentation
            {
                Id = entity.Id,
                ControlMethodDocumentation_id = entity.ControlMethodDocumentation.Id,
            };

            return dalEntity;
        }

        public BllSelectedControlMethodDocumentation MapToBll(DalSelectedControlMethodDocumentation entity)
        {
            ControlMethodDocumentationService controlMethodDocumentationService = new ControlMethodDocumentationService(uow);
            var bllControlMethodDocumentation = entity.ControlMethodDocumentation_id != null ? controlMethodDocumentationService.Get((int)entity.ControlMethodDocumentation_id) : null;

            BllSelectedControlMethodDocumentation bllEntity = new BllSelectedControlMethodDocumentation
            {
                Id = entity.Id,
                ControlMethodDocumentation = bllControlMethodDocumentation
            };

            return bllEntity;
        }
    }
}
