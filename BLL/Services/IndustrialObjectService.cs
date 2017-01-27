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
    public class IndustrialObjectService : Service<BllIndustrialObject, DalIndustrialObject>, IIndustrialObjectService
    {
        private readonly IUnitOfWork uow;

        public IndustrialObjectService(IUnitOfWork uow) : base(uow, uow.IndustrialObjects)
        {
            this.uow = uow;
        }

        public BllIndustrialObject GetIndustrialObjectByName(string name)
        {
            Mapper.CreateMap<DalIndustrialObject, BllIndustrialObject>();
            return Mapper.Map<BllIndustrialObject>(uow.IndustrialObjects.GetIndustrialObjectByName(name));
        }

        public override void Create(BllIndustrialObject entity)
        {
            ComponentLibService ComponentLibService = new ComponentLibService(uow);
            var ComponentLib = ComponentLibService.Create(entity.ComponentLib);
            entity.ComponentLib = ComponentLib;
            uow.IndustrialObjects.Create(MapBllToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllIndustrialObject entity)
        {
            uow.IndustrialObjects.Delete(MapBllToDal(entity));
            uow.Commit();
        }

        public override void Update(BllIndustrialObject entity)
        {
            ComponentLibService ComponentLibService = new ComponentLibService(uow);
            ComponentLibService.Update(entity.ComponentLib);
            uow.IndustrialObjects.Update(MapBllToDal(entity));
            uow.Commit();
        }

        public override BllIndustrialObject Get(int id)
        {
            DalIndustrialObject dalEntity = uow.IndustrialObjects.Get(id);
            return MapDalToBll(dalEntity);
        }

        public override IEnumerable<BllIndustrialObject> GetAll()
        {
            var elements = uow.IndustrialObjects.GetAll();
            var retElemets = new List<BllIndustrialObject>();
            foreach (var element in elements)
            {
                retElemets.Add(MapDalToBll(element));
            }
            return retElemets;
        }

        private DalIndustrialObject MapBllToDal(BllIndustrialObject entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllIndustrialObject, DalIndustrialObject>();
            });

            DalIndustrialObject dalEntity = Mapper.Map<DalIndustrialObject>(entity);
            dalEntity.Component_lib_id = entity.ComponentLib != null ? entity.ComponentLib.Id : (int?)null;
            return dalEntity;
        }

        private BllIndustrialObject MapDalToBll(DalIndustrialObject entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DalIndustrialObject, BllIndustrialObject>();
            });
            BllIndustrialObject bllIndustrialObject = Mapper.Map<BllIndustrialObject>(entity);
            ComponentLibService ComponentLibService = new ComponentLibService(uow);
            bllIndustrialObject.ComponentLib = entity.Component_lib_id != null ? ComponentLibService.Get((int)entity.Component_lib_id) : null;
            return bllIndustrialObject;
        }
    }
}
