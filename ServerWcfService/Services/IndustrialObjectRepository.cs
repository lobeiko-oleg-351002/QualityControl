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
    public class IndustrialObjectRepository : Repository<UilIndustrialObject, BllIndustrialObject>, IIndustrialObjectRepository
    {
        private readonly IIndustrialObjectService IndustrialObjectService;

        public IndustrialObjectRepository() : base(UiUnitOfWork.Instance.IndustrialObjects)
        {
            IndustrialObjectService = UiUnitOfWork.Instance.IndustrialObjects;
        }

        public override void Create(UilIndustrialObject entity)
        {
            IndustrialObjectService.Create(MapUilToBll(entity));
        }

        public override void Delete(UilIndustrialObject entity)
        {
            IndustrialObjectService.Delete(MapUilToBll(entity));
        }

        public override void Update(UilIndustrialObject entity)
        {
            IndustrialObjectService.Update(MapUilToBll(entity));
        }

        public override IEnumerable<UilIndustrialObject> GetAll()
        {
            var bllEntities = IndustrialObjectService.GetAll();
            var retElements = new List<UilIndustrialObject>();
            foreach (var element in bllEntities)
            {
                retElements.Add(MapBllToUil(element));
            }
            return retElements;
        }

        public static UilIndustrialObject MapBllToUil(BllIndustrialObject bllEntity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllIndustrialObject, UilIndustrialObject>();
                cfg.CreateMap<BllComponentLib, UilComponentLib>();
                cfg.CreateMap<BllSelectedComponent, UilSelectedComponent>().ForMember(x => x.Component, opt => opt.Ignore());
            });
            UilIndustrialObject uilEntity = Mapper.Map<UilIndustrialObject>(bllEntity);
            if (bllEntity.ComponentLib != null)
            {
                for (int i = 0; i < bllEntity.ComponentLib.SelectedComponent.Count; i++)
                {
                    uilEntity.ComponentLib.SelectedComponent[i].Component = ComponentRepository.MapBllToUil(bllEntity.ComponentLib.SelectedComponent[i].Component);
                }
            }

            return uilEntity;
        }

        public static BllIndustrialObject MapUilToBll(UilIndustrialObject entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UilIndustrialObject, BllIndustrialObject>();
                cfg.CreateMap<UilComponentLib, BllComponentLib>();
                cfg.CreateMap<UilSelectedComponent, BllSelectedComponent>().ForMember(x => x.Component, opt => opt.Ignore());
            });

            BllIndustrialObject bllEntity = Mapper.Map<BllIndustrialObject>(entity);
            if (entity.ComponentLib != null)
            {
                for (int i = 0; i < entity.ComponentLib.SelectedComponent.Count; i++)
                {
                    bllEntity.ComponentLib.SelectedComponent[i].Component = ComponentRepository.MapUilToBll(entity.ComponentLib.SelectedComponent[i].Component);
                }
            }
            return bllEntity;
        }
    }
}
