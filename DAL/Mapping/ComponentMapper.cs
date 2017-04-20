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
    public class ComponentMapper : IComponentMapper
    {
        public DalComponent MapToDal(Component entity)
        {
            return new DalComponent
            {
                Id = entity.id,
                Name = entity.name,
                Pressmark = entity.pressmark,
                Template_id = entity.template_id
            };
        }

        public Component MapToOrm(DalComponent entity)
        {
            return new Component
            {
                id = entity.Id,
                name = entity.Name,
                pressmark = entity.Pressmark,
                template_id = entity.Template_id
            };
        }
    }
}
