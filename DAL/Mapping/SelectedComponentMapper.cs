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
    public class SelectedComponentMapper : ISelectedComponentMapper
    {
        public DalSelectedComponent MapToDal(SelectedComponent entity)
        {
            return new DalSelectedComponent
            {
                Id = entity.id,
                ComponentLib_id = entity.componentLib_id,
                Component_id = entity.component_id
            };
        }

        public SelectedComponent MapToOrm(DalSelectedComponent entity)
        {
            return new SelectedComponent
            {
                id = entity.Id,
                component_id = entity.Component_id,
                componentLib_id = entity.ComponentLib_id
            };
        }
    }
}
