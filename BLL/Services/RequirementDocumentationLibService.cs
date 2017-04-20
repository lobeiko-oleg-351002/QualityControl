using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
using BLL.Mapping.Interfaces;
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
        IRequirementDocumentationLibMapper bllMapper;
        public RequirementDocumentationLibService(IUnitOfWork uow) : base(uow, uow.RequirementDocumentationLibs)
        {
            this.uow = uow;
            bllMapper = new RequirementDocumentationLibMapper(uow);
        }

        public new BllRequirementDocumentationLib Create(BllRequirementDocumentationLib entity)
        {
            var ormEntity = uow.RequirementDocumentationLibs.Create(bllMapper.MapToDal(entity));
            uow.Commit();
            entity.Id = ormEntity.id;
            ISelectedRequirementDocumentationMapper selectedRequirementDocumentationMapper = new SelectedRequirementDocumentationMapper(uow);
            foreach (var RequirementDocumentation in entity.SelectedRequirementDocumentation)
            {
                var dalRequirementDocumentation = selectedRequirementDocumentationMapper.MapToDal(RequirementDocumentation);
                dalRequirementDocumentation.RequirementDocumentationLib_id = entity.Id;
                var ormDoc = uow.SelectedRequirementDocumentations.Create(dalRequirementDocumentation);
                uow.Commit();
                RequirementDocumentation.Id = ormDoc.id;
            }
            return entity;
        }

        public override BllRequirementDocumentationLib Get(int id)
        {
            return bllMapper.MapToBll(uow.RequirementDocumentationLibs.Get(id));
        }

        public new BllRequirementDocumentationLib Update(BllRequirementDocumentationLib entity)
        {
            ISelectedRequirementDocumentationMapper selectedRequirementDocumentationMapper = new SelectedRequirementDocumentationMapper(uow);
            foreach (var RequirementDocumentation in entity.SelectedRequirementDocumentation)
            {
                if (RequirementDocumentation.Id == 0)
                {
                    var dalRequirementDocumentation = selectedRequirementDocumentationMapper.MapToDal(RequirementDocumentation);
                    dalRequirementDocumentation.RequirementDocumentationLib_id = entity.Id;
                    var ormDoc =  uow.SelectedRequirementDocumentations.Create(dalRequirementDocumentation);
                    uow.Commit();
                    RequirementDocumentation.Id = ormDoc.id;
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

            return entity;
        }

        //private BllRequirementDocumentationLib MapDalToBll(DalRequirementDocumentationLib dalEntity)
        //{
        //    Mapper.Initialize(cfg =>
        //    {
        //        cfg.CreateMap<DalRequirementDocumentationLib, BllRequirementDocumentationLib>();
        //        cfg.CreateMap<DalRequirementDocumentation, BllRequirementDocumentation>();
        //        cfg.CreateMap<DalSelectedRequirementDocumentation, BllSelectedRequirementDocumentation>();
        //    });
        //    BllRequirementDocumentationLib bllEntity = Mapper.Map<BllRequirementDocumentationLib>(dalEntity);

        //    SelectedRequirementDocumentationService selectedRequirementDocumentationService = new SelectedRequirementDocumentationService(uow);
        //    RequirementDocumentationService RequirementDocumentationService = new RequirementDocumentationService(uow);
        //    foreach (var RequirementDocumentation in uow.SelectedRequirementDocumentations.GetRequirementDocumentationsByLibId(dalEntity.Id))
        //    {
        //        var bllRequirementDocumentation = RequirementDocumentation.RequirementDocumentation_id != null ? RequirementDocumentationService.Get((int)RequirementDocumentation.RequirementDocumentation_id) : null;
        //        var bllSelectedRequirementDocumentation = Mapper.Map<BllSelectedRequirementDocumentation>(RequirementDocumentation);
        //        bllSelectedRequirementDocumentation.RequirementDocumentation = bllRequirementDocumentation;
        //        bllEntity.SelectedRequirementDocumentation.Add(bllSelectedRequirementDocumentation);

        //    }
        //    return bllEntity;
        //}
    }
}