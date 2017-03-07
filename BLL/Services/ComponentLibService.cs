using AutoMapper;
using BLL.Entities;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ComponentLibService : Service<BllComponentLib, DalComponentLib>, IComponentLibService
    {
        private readonly IUnitOfWork uow;

        public ComponentLibService(IUnitOfWork uow) : base(uow, uow.ComponentLibs)
        {
            this.uow = uow;
        }

        public new BllComponentLib Create(BllComponentLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllSelectedComponent, DalSelectedComponent>();
                cfg.CreateMap<ComponentLib, DalComponentLib>();
                cfg.CreateMap<BllComponentLib, DalComponentLib>();
                cfg.CreateMap<DalComponentLib, BllComponentLib>();
            });
            var ormEntity = uow.ComponentLibs.Create(Mapper.Map<DalComponentLib>(entity));
            uow.Commit();
            var dalEntity = Mapper.Map<DalComponentLib>(ormEntity);
            foreach (var Component in entity.SelectedComponent)
            {
                Mapper.CreateMap<BllSelectedComponent, DalSelectedComponent>();
                var dalComponent = Mapper.Map<DalSelectedComponent>(Component);
                dalComponent.ComponentLib_id = dalEntity.Id;
                uow.SelectedComponents.Create(dalComponent);
            }
            uow.Commit();
            Mapper.CreateMap<DalComponentLib, BllComponentLib>();
            return Mapper.Map<BllComponentLib>(dalEntity);
        }

        public override BllComponentLib Get(int id)
        {
            Mapper.CreateMap<DalComponentLib, BllComponentLib>();
            return MapDalToBll(uow.ComponentLibs.Get(id));
        }

        public override void Update(BllComponentLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllSelectedComponent, DalSelectedComponent>();
                cfg.CreateMap<ComponentLib, DalComponentLib>();
                cfg.CreateMap<BllComponentLib, DalComponentLib>();
                cfg.CreateMap<DalComponentLib, BllComponentLib>();
            });
            foreach (var Component in entity.SelectedComponent)
            {
                if (Component.Id == 0)
                {
                    var dalComponent = Mapper.Map<DalSelectedComponent>(Component);
                    dalComponent.ComponentLib_id = entity.Id;
                    uow.SelectedComponents.Create(dalComponent);
                }
            }
            var ComponentsWithLibId = uow.SelectedComponents.GetComponentsByLibId(entity.Id);
            foreach (var Component in ComponentsWithLibId)
            {
                bool isTrashComponent = true;
                foreach (var selectedComponent in entity.SelectedComponent)
                {
                    if (Component.Id == selectedComponent.Id)
                    {
                        isTrashComponent = false;
                        break;
                    }
                }
                if (isTrashComponent == true)
                {
                    uow.SelectedComponents.Delete(Component);
                }
            }
            uow.Commit();
        }

        private BllComponentLib MapDalToBll(DalComponentLib dalEntity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DalComponentLib, BllComponentLib>();
                cfg.CreateMap<DalComponent, BllComponent>();
                cfg.CreateMap<DalSelectedComponent, BllSelectedComponent>();
            });
            BllComponentLib bllEntity = Mapper.Map<BllComponentLib>(dalEntity);

            SelectedComponentService selectedComponentService = new SelectedComponentService(uow);
            ComponentService ComponentService = new ComponentService(uow);
            foreach (var Component in uow.SelectedComponents.GetComponentsByLibId(dalEntity.Id))
            {
                var bllComponent = Component.Component_id != null ? ComponentService.Get((int)Component.Component_id) : null;
                Mapper.CreateMap<DalSelectedComponent, BllSelectedComponent>();
                var bllSelectedComponent = Mapper.Map<BllSelectedComponent>(Component);
                bllSelectedComponent.Component = bllComponent;
                bllEntity.SelectedComponent.Add(bllSelectedComponent);

            }
            return bllEntity;
        }
    }
}
