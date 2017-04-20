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
    public class MaterialMapper : IMaterialMapper
    {

        public MaterialMapper()
        {

        }

        public DalMaterial MapToDal(BllMaterial entity)
        {
            DalMaterial dalEntity = new DalMaterial
            {
                Id = entity.Id,
                Description = entity.Description,
                Name = entity.Name
            };

            return dalEntity;
        }

        public BllMaterial MapToBll(DalMaterial entity)
        {
            BllMaterial bllEntity = new BllMaterial
            {
                Id = entity.Id,
                Description = entity.Description,
                Name = entity.Name
            };

            return bllEntity;
        }
    }
}
