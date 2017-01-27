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
    public class RequirementDocumentationLibService : Service<BllRequirementDocumentationLib, DalRequirementDocumentationLib>, IRequirementDocumentationLibService
    {
        private readonly IUnitOfWork uow;

        public RequirementDocumentationLibService(IUnitOfWork uow) : base(uow, uow.RequirementDocumentationLibs)
        {
            this.uow = uow;
        }

        public new BllRequirementDocumentationLib Create(BllRequirementDocumentationLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllSelectedRequirementDocumentation, DalSelectedRequirementDocumentation>();
                cfg.CreateMap<RequirementDocumentationLib, DalRequirementDocumentationLib>();
                cfg.CreateMap<BllRequirementDocumentationLib, DalRequirementDocumentationLib>();
                cfg.CreateMap<DalRequirementDocumentationLib, BllRequirementDocumentationLib>();
            });
            var ormEntity = uow.RequirementDocumentationLibs.Create(Mapper.Map<DalRequirementDocumentationLib>(entity));
            uow.Commit();
            entity.Id = ormEntity.id;
            foreach (var RequirementDocumentation in entity.SelectedRequirementDocumentation)
            {
                Mapper.CreateMap<BllSelectedRequirementDocumentation, DalSelectedRequirementDocumentation>();
                var dalRequirementDocumentation = Mapper.Map<DalSelectedRequirementDocumentation>(RequirementDocumentation);
                dalRequirementDocumentation.RequirementDocumentationLib_id = entity.Id;
                var ormDoc = uow.SelectedRequirementDocumentations.Create(dalRequirementDocumentation);
                uow.Commit();
                RequirementDocumentation.Id = ormDoc.id;
            }
            return entity;
        }

        public override BllRequirementDocumentationLib Get(int id)
        {
            Mapper.CreateMap<DalRequirementDocumentationLib, BllRequirementDocumentationLib>();
            return MapDalToBll(uow.RequirementDocumentationLibs.Get(id));
        }

        public override void Update(BllRequirementDocumentationLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllSelectedRequirementDocumentation, DalSelectedRequirementDocumentation>();
                cfg.CreateMap<RequirementDocumentationLib, DalRequirementDocumentationLib>();
                cfg.CreateMap<BllRequirementDocumentationLib, DalRequirementDocumentationLib>();
                cfg.CreateMap<DalRequirementDocumentationLib, BllRequirementDocumentationLib>();
            });
            foreach (var RequirementDocumentation in entity.SelectedRequirementDocumentation)
            {
                if (RequirementDocumentation.Id == 0)
                {
                    var dalRequirementDocumentation = Mapper.Map<DalSelectedRequirementDocumentation>(RequirementDocumentation);
                    dalRequirementDocumentation.RequirementDocumentationLib_id = entity.Id;
                    uow.SelectedRequirementDocumentations.Create(dalRequirementDocumentation);
                }
            }
            var RequirementDocumentationsWithLibId = uow.SelectedRequirementDocumentations.GetRequirementDocumentationsByLibId(entity.Id);
            foreach (var RequirementDocumentation in RequirementDocumentationsWithLibId)
            {
                bool isTrashRequirementDocumentation = true;
                foreach (var selectedRequirementDocumentation in entity.SelectedRequirementDocumentation)
                {
                    if (RequirementDocumentation.Id == selectedRequirementDocumentation.Id)
                    {
                        isTrashRequirementDocumentation = false;
                        break;
                    }
                }
                if (isTrashRequirementDocumentation == true)
                {
                    uow.SelectedRequirementDocumentations.Delete(RequirementDocumentation);
                }
            }
            uow.Commit();
        }

        private BllRequirementDocumentationLib MapDalToBll(DalRequirementDocumentationLib dalEntity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DalRequirementDocumentationLib, BllRequirementDocumentationLib>();
                cfg.CreateMap<DalRequirementDocumentation, BllRequirementDocumentation>();
                cfg.CreateMap<DalSelectedRequirementDocumentation, BllSelectedRequirementDocumentation>();
            });
            BllRequirementDocumentationLib bllEntity = Mapper.Map<BllRequirementDocumentationLib>(dalEntity);

            SelectedRequirementDocumentationService selectedRequirementDocumentationService = new SelectedRequirementDocumentationService(uow);
            RequirementDocumentationService RequirementDocumentationService = new RequirementDocumentationService(uow);
            foreach (var RequirementDocumentation in uow.SelectedRequirementDocumentations.GetRequirementDocumentationsByLibId(dalEntity.Id))
            {
                var bllRequirementDocumentation = RequirementDocumentation.RequirementDocumentation_id != null ? RequirementDocumentationService.Get((int)RequirementDocumentation.RequirementDocumentation_id) : null;
                var bllSelectedRequirementDocumentation = Mapper.Map<BllSelectedRequirementDocumentation>(RequirementDocumentation);
                bllSelectedRequirementDocumentation.RequirementDocumentation = bllRequirementDocumentation;
                bllEntity.SelectedRequirementDocumentation.Add(bllSelectedRequirementDocumentation);

            }
            return bllEntity;
        }
    }
}