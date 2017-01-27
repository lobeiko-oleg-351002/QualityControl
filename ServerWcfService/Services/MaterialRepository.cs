using AutoMapper;
using BLL.Entities;
using BLL.Services.Interface;
using ServerWcfService.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities;

namespace ServerWcfService.Services
{
    public class MaterialRepository : Repository<UilMaterial, BllMaterial>, IMaterialRepository
    {
        private readonly IMaterialService materialService;

        public MaterialRepository() : base(UiUnitOfWork.Instance.Materials)
        {
            materialService = UiUnitOfWork.Instance.Materials;
        }

        public UilMaterial GetMaterialByName(string name)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllMaterial, UilMaterial>();
            });
            return Mapper.Map<UilMaterial>(materialService.GetMaterialByName(name));
        }
    }
}
