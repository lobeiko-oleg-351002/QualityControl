using BLL.Entities;
using BLL.Mapping.Interfaces;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class ControlMethodDocumentationMapper : IControlMethodDocumentationMapper
    {
        IUnitOfWork uow;
        public ControlMethodDocumentationMapper(IUnitOfWork uow)
        {
            this.uow = uow;
            controlNameService = new ControlNameService(uow);
        }

        public DalControlMethodDocumentation MapToDal(BllControlMethodDocumentation entity)
        {
            DalControlMethodDocumentation dalEntity = new DalControlMethodDocumentation
            {
                Id = entity.Id,
                Name = entity.Name,
                Pressmark = entity.Pressmark,
                ControlName_id = entity.ControlName != null ? entity.ControlName.Id : (int?)null
            };

            return dalEntity;
        }

        IControlNameService controlNameService;

        public BllControlMethodDocumentation MapToBll(DalControlMethodDocumentation entity)
        {
            BllControlMethodDocumentation bllEntity = new BllControlMethodDocumentation
            {
                Id = entity.Id,
                Name = entity.Name,
                Pressmark = entity.Pressmark,
                ControlName = entity.ControlName_id != null ? controlNameService.Get((int)entity.ControlName_id) : null,
            };

            return bllEntity;
        }
    }
}
