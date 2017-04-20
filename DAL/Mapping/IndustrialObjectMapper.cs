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
    public class IndustrialObjectMapper : IIndustrialObjectMapper
    {
        public DalIndustrialObject MapToDal(IndustrialObject entity)
        {
            return new DalIndustrialObject
            {
                Id = entity.id,
                ComponentLib_id = entity.componentLib_id,
                Name = entity.name,
               
            };
        }

        public IndustrialObject MapToOrm(DalIndustrialObject entity)
        {
            return new IndustrialObject
            {
                id = entity.Id,
                componentLib_id = entity.ComponentLib_id,
                name = entity.Name
            };
        }
    }
}
