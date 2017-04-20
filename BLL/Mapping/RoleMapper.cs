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
    public class RoleMapper : IRoleMapper
    {

        public RoleMapper()
        {
        }

        public DalRole MapToDal(BllRole entity)
        {
            DalRole dalEntity = new DalRole
            {
                Id = entity.Id,
                Name = entity.Name
            };

            return dalEntity;
        }


        public BllRole MapToBll(DalRole entity)
        {
            BllRole bllEntity = new BllRole
            {
                Id = entity.Id,
                Name = entity.Name
            };

            return bllEntity;
        }
    }
}
