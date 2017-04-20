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
    public class IndustrialObjectMapper : IIndustrialObjectMapper
    {
        IUnitOfWork uow;
        public IndustrialObjectMapper(IUnitOfWork uow)
        {
            this.uow = uow;
            componentLibService = new ComponentLibService(uow);
        }

        public DalIndustrialObject MapToDal(BllIndustrialObject entity)
        {
            DalIndustrialObject dalEntity = new DalIndustrialObject
            {
                Id = entity.Id,
                Name = entity.Name,
                ComponentLib_id = entity.ComponentLib != null ? entity.ComponentLib.Id : (int?)null,
            };

            return dalEntity;
        }

        IComponentLibService componentLibService;

        public BllIndustrialObject MapToBll(DalIndustrialObject entity)
        {
            BllIndustrialObject bllEntity = new BllIndustrialObject
            {
                Id = entity.Id,
                Name = entity.Name,
                ComponentLib = entity.ComponentLib_id != null ? componentLibService.Get((int)entity.ComponentLib_id) : null,
            };

            return bllEntity;
        }
    }
}
