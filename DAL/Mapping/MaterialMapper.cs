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
    public class MaterialMapper : IMaterialMapper
    {
        public DalMaterial MapToDal(Material entity)
        {
            return new DalMaterial
            {
                Id = entity.id,
                Description = entity.description,
                Name = entity.name
            };
        }

        public Material MapToOrm(DalMaterial entity)
        {
            return new Material
            {
                id = entity.Id,
                description = entity.Description,
                name = entity.Name
            };
        }
    }
}
