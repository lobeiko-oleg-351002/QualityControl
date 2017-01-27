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
    public class ComponentRepository : Repository<UilComponent, BllComponent>, IComponentRepository
    {
        private readonly IComponentService componentService;

        public ComponentRepository() : base(UiUnitOfWork.Instance.Components)
        {
            componentService = UiUnitOfWork.Instance.Components;
        }

        public override void Create(UilComponent entity)
        {
            componentService.Create(MapUilToBll(entity));
        }

        public override void Delete(UilComponent entity)
        {
            componentService.Delete(MapUilToBll(entity));
        }

        public override void Update(UilComponent entity)
        {
            componentService.Update(MapUilToBll(entity));
        }

        public override IEnumerable<UilComponent> GetAll()
        {
            var bllEntities = componentService.GetAll();
            var retElements = new List<UilComponent>();
            foreach (var element in bllEntities)
            {
                retElements.Add(MapBllToUil(element));
            }
            return retElements;
        }

        public static UilComponent MapBllToUil(BllComponent bllEntity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllComponent, UilComponent>().ForMember(x => x.Template, opt => opt.Ignore());
            });
            UilComponent uilEntity = Mapper.Map<UilComponent>(bllEntity);
            uilEntity.Template = bllEntity.Template != null ? TemplateRepository.MapBllToUil(bllEntity.Template) : null;
            return uilEntity;
        }

        public static BllComponent MapUilToBll(UilComponent entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UilComponent, BllComponent>().ForMember(x => x.Template, opt => opt.Ignore());
            });

            BllComponent bllEntity = Mapper.Map<BllComponent>(entity);
            bllEntity.Template = entity.Template != null ? TemplateRepository.MapUilToBll(entity.Template) : null;

            return bllEntity;
        }
    }
}
