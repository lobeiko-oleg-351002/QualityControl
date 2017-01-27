using AutoMapper;
using BLL.Entities;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ComponentService : Service<BllComponent, DalComponent>, IComponentService
    {
        private readonly IUnitOfWork uow;

        public ComponentService(IUnitOfWork uow) : base(uow, uow.Components)
        {
            this.uow = uow;
        }

        public override void Create(BllComponent entity)
        {
            uow.Components.Create(MapBllToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllComponent entity)
        {
            uow.Components.Delete(MapBllToDal(entity));
            uow.Commit();
        }

        public override IEnumerable<BllComponent> GetAll()
        {
            var elements = uow.Components.GetAll();
            var retElemets = new List<BllComponent>();
            foreach (var element in elements)
            {
                retElemets.Add(MapDalToBll(element));
            }
            return retElemets;
        }

        public override BllComponent Get(int id)
        {
            return MapDalToBll(uow.Components.Get(id));
        }

        public BllComponent GetComponentByName(string name)
        {
            Mapper.CreateMap<DalComponent, BllComponent>();
            return Mapper.Map<BllComponent>(uow.Components.GetComponentByName(name));
        }

        public IEnumerable<BllComponent> GetComponentsByTemplateId(int id)
        {
            Mapper.CreateMap<DalComponent, BllComponent>();
            var elements = uow.Components.GetComponentsByTemplateId(id);
            var retElemets = new List<BllComponent>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<BllComponent>(element));
            }
            return retElemets;
        }

        private DalComponent MapBllToDal(BllComponent entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllComponent, DalComponent>();
            });

            DalComponent dalEntity = Mapper.Map<DalComponent>(entity);
            dalEntity.Template_id = entity.Template != null ? entity.Template.Id : (int?)null;
            return dalEntity;
        }

        private BllComponent MapDalToBll(DalComponent entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DalComponent, BllComponent>();
            });
            BllComponent bllComponent = Mapper.Map<BllComponent>(entity);
            TemplateService templateService = new TemplateService(uow);
            bllComponent.Template = entity.Template_id != null ? templateService.Get((int)entity.Template_id) : null;
            return bllComponent;
        }
    }
}
