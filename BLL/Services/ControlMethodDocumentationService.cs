using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
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
    public class ControlMethodDocumentationService : Service<BllControlMethodDocumentation, DalControlMethodDocumentation>, IControlMethodDocumentationService
    {
        private readonly IUnitOfWork uow;
        ControlMethodDocumentationMapper bllMapper;
        public ControlMethodDocumentationService(IUnitOfWork uow) : base(uow, uow.ControlMethodDocumentations)
        {
            this.uow = uow;
            bllMapper = new ControlMethodDocumentationMapper(uow);
        }

        public BllControlMethodDocumentation GetControlMethodDocumentationByName(string name)
        {
            return bllMapper.MapToBll(uow.ControlMethodDocumentations.GetControlMethodDocumentationByName(name));
        }

        public override void Create(BllControlMethodDocumentation entity)
        {
            uow.ControlMethodDocumentations.Create(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Update(BllControlMethodDocumentation entity)
        {
            uow.ControlMethodDocumentations.Update(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllControlMethodDocumentation entity)
        {
            uow.ControlMethodDocumentations.Delete(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override IEnumerable<BllControlMethodDocumentation> GetAll()
        {
            var elements = uow.ControlMethodDocumentations.GetAll();
            var retElemets = new List<BllControlMethodDocumentation>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }

        public override BllControlMethodDocumentation Get(int id)
        {
            return bllMapper.MapToBll(uow.ControlMethodDocumentations.Get(id));
        }

        //private DalControlMethodDocumentation MapBllToDal(BllControlMethodDocumentation entity)
        //{
        //    Mapper.Initialize(cfg =>
        //    {
        //        cfg.CreateMap<BllControlMethodDocumentation, DalControlMethodDocumentation>();
        //    });

        //    DalControlMethodDocumentation dalEntity = Mapper.Map<DalControlMethodDocumentation>(entity);
        //    dalEntity.ControlName_id = entity.ControlName != null ? entity.ControlName.Id : (int?)null;
        //    return dalEntity;
        //}

        //private BllControlMethodDocumentation MapDalToBll(DalControlMethodDocumentation entity)
        //{
        //    Mapper.Initialize(cfg =>
        //    {
        //        cfg.CreateMap<DalControlMethodDocumentation, BllControlMethodDocumentation>();
        //    });
        //    BllControlMethodDocumentation bllControlMethodDocumentation = Mapper.Map<BllControlMethodDocumentation>(entity);
        //    ControlNameService ControlNameService = new ControlNameService(uow);
        //    bllControlMethodDocumentation.ControlName = entity.ControlName_id != null ? ControlNameService.Get((int)entity.ControlName_id) : null;
        //    return bllControlMethodDocumentation;
        //}
    }
}
