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
    public class ControlMethodDocumentationLibService : Service<BllControlMethodDocumentationLib, DalControlMethodDocumentationLib>, IControlMethodDocumentationLibService
    {
        private readonly IUnitOfWork uow;

        ControlMethodDocumentationLibMapper bllMapper;

        public ControlMethodDocumentationLibService(IUnitOfWork uow) : base(uow, uow.ControlMethodDocumentationLibs)
        {
            this.uow = uow;
            bllMapper = new ControlMethodDocumentationLibMapper(uow);
        }

        public new BllControlMethodDocumentationLib Create(BllControlMethodDocumentationLib entity)
        {
            ISelectedControlMethodDocumentationMapper selectedControlMethodDocumentationMapper = new SelectedControlMethodDocumentationMapper(uow);
            var ormEntity = uow.ControlMethodDocumentationLibs.Create(bllMapper.MapToDal(entity));
            uow.Commit();
            entity.Id = ormEntity.id;
            foreach (var ControlMethodDocumentation in entity.SelectedControlMethodDocumentation)
            {
                var dalControlMethodDocumentation = selectedControlMethodDocumentationMapper.MapToDal(ControlMethodDocumentation);
                dalControlMethodDocumentation.ControlMethodDocumentationLib_id = entity.Id;
                var ormMethod = uow.SelectedControlMethodDocumentations.Create(dalControlMethodDocumentation);
                uow.Commit();
                ControlMethodDocumentation.Id = ormMethod.id;
            }

            return entity;
        }

        public override BllControlMethodDocumentationLib Get(int id)
        {
            return bllMapper.MapToBll(uow.ControlMethodDocumentationLibs.Get(id));
        }

        public new BllControlMethodDocumentationLib Update(BllControlMethodDocumentationLib entity)
        {
            ISelectedControlMethodDocumentationMapper selectedControlMethodDocumentationMapper = new SelectedControlMethodDocumentationMapper(uow);
            foreach (var ControlMethodDocumentation in entity.SelectedControlMethodDocumentation)
            {
                if (ControlMethodDocumentation.Id == 0)
                {
                    var dalControlMethodDocumentation = selectedControlMethodDocumentationMapper.MapToDal(ControlMethodDocumentation);
                    dalControlMethodDocumentation.ControlMethodDocumentationLib_id = entity.Id;
                    var ormDoc = uow.SelectedControlMethodDocumentations.Create(dalControlMethodDocumentation);
                    uow.Commit();
                    ControlMethodDocumentation.Id = ormDoc.id;
                }
            }
            var ControlMethodDocumentationsWithLibId = uow.SelectedControlMethodDocumentations.GetControlMethodDocumentationsByLibId(entity.Id);
            foreach (var ControlMethodDocumentation in ControlMethodDocumentationsWithLibId)
            {
                bool isTrashControlMethodDocumentation = true;
                foreach (var selectedControlMethodDocumentation in entity.SelectedControlMethodDocumentation)
                {
                    if (ControlMethodDocumentation.Id == selectedControlMethodDocumentation.Id)
                    {
                        isTrashControlMethodDocumentation = false;
                        break;
                    }
                }
                if (isTrashControlMethodDocumentation == true)
                {
                    uow.SelectedControlMethodDocumentations.Delete(ControlMethodDocumentation);
                }
            }
            uow.Commit();

            return entity;
        }

        //private BllControlMethodDocumentationLib MapDalToBll(DalControlMethodDocumentationLib dalEntity)
        //{
        //    Mapper.Initialize(cfg =>
        //    {
        //        cfg.CreateMap<DalControlMethodDocumentationLib, BllControlMethodDocumentationLib>();
        //        cfg.CreateMap<DalControlMethodDocumentation, BllControlMethodDocumentation>();
        //        cfg.CreateMap<DalSelectedControlMethodDocumentation, BllSelectedControlMethodDocumentation>();
        //    });
        //    BllControlMethodDocumentationLib bllEntity = Mapper.Map<BllControlMethodDocumentationLib>(dalEntity);

        //    SelectedControlMethodDocumentationService selectedControlMethodDocumentationService = new SelectedControlMethodDocumentationService(uow);
        //    ControlMethodDocumentationService ControlMethodDocumentationService = new ControlMethodDocumentationService(uow);
        //    foreach (var ControlMethodDocumentation in uow.SelectedControlMethodDocumentations.GetControlMethodDocumentationsByLibId(dalEntity.Id))
        //    {
        //        var bllControlMethodDocumentation = ControlMethodDocumentation.ControlMethodDocumentation_id != null ? ControlMethodDocumentationService.Get((int)ControlMethodDocumentation.ControlMethodDocumentation_id) : null;
        //        Mapper.Initialize(cfg =>
        //        {
        //            cfg.CreateMap<DalSelectedControlMethodDocumentation, BllSelectedControlMethodDocumentation>();
        //        });
        //        var bllSelectedControlMethodDocumentation = Mapper.Map<BllSelectedControlMethodDocumentation>(ControlMethodDocumentation);
        //        bllSelectedControlMethodDocumentation.ControlMethodDocumentation = bllControlMethodDocumentation;
        //        bllEntity.SelectedControlMethodDocumentation.Add(bllSelectedControlMethodDocumentation);

        //    }
        //    return bllEntity;
        //}
    
    }
}