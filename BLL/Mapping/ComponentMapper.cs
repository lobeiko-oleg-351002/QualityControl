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
    public class ComponentMapper : IComponentMapper
    {
        IUnitOfWork uow;
        public ComponentMapper(IUnitOfWork uow)
        {
            this.uow = uow;
            templateService = new TemplateService(uow);
        }

        public DalComponent MapToDal(BllComponent entity)
        {
            return new DalComponent
            {
                Id = entity.Id,
                Name = entity.Name,
                Pressmark = entity.Pressmark,
                Template_id = entity.Template != null ? entity.Template.Id : (int?)null               
            };
        }

        ITemplateService templateService;

        public BllComponent MapToBll(DalComponent entity)
        {
            BllComponent bllEntity = new BllComponent
            {
                Id = entity.Id,
                Name = entity.Name,
                Pressmark = entity.Pressmark,
                Template = entity.Template_id != null ? templateService.Get((int)entity.Template_id) : null
            };

            return bllEntity;
        }
    }
}
