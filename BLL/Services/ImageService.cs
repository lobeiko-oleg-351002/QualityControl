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
    public class ImageService : Service<BllImage, DalImage>, IImageService
    {
        private readonly IUnitOfWork uow;

        public ImageService(IUnitOfWork uow) : base(uow, uow.Images)
        {
            this.uow = uow;
        }

        //public override void Create(BllImage entity)
        //{
        //    Mapper.CreateMap<BllImage, DalImage>();
        //    DalImage dalEntity = Mapper.Map<DalImage>(entity);
        //    dalEntity.ImageLib_id = entity.ImageLib.Id;
        //    uow.Images.Create(dalEntity);
        //    uow.Commit();
        //}

        //public override void Delete(BllImage entity)
        //{
        //    Mapper.CreateMap<BllImage, DalImage>();
        //    DalImage dalEntity = Mapper.Map<DalImage>(entity);
        //    dalEntity.ImageLib_id = entity.ImageLib.Id;
        //    uow.Images.Delete(dalEntity);
        //    uow.Commit();
        //}

        //public override void Update(BllImage entity)
        //{
        //    Mapper.CreateMap<BllImage, DalImage>();
        //    DalImage dalEntity = Mapper.Map<DalImage>(entity);
        //    dalEntity.ImageLib_id = entity.ImageLib.Id;
        //    uow.Images.Create(dalEntity);
        //    uow.Commit();
        //}

        //public override BllImage Get(int id)
        //{
        //    Mapper.CreateMap<DalImage, BllImage>();
        //    DalImage dalEntity = uow.Images.Get(id);
        //    BllImage bllEntity = Mapper.Map<BllImage>(dalEntity);
        //    ImageLibService imageLibService = new ImageLibService(uow);
        //    bllEntity.ImageLib = dalEntity.ImageLib_id != null ? imageLibService.Get((int)dalEntity.ImageLib_id) : null;
        //    return bllEntity;
        //}

        //public override IEnumerable<BllImage> GetAll()
        //{
        //    Mapper.CreateMap<DalImage, BllImage>();
        //    var elements = uow.Images.GetAll();
        //    var retElemets = new List<BllImage>();
        //    ImageLibService imageLibService = new ImageLibService(uow);
        //    foreach (var element in elements)
        //    {
        //        BllImage bllEntity = Mapper.Map<BllImage>(element);
        //        bllEntity.ImageLib = element.ImageLib_id != null ? imageLibService.Get((int)element.ImageLib_id) : null;
        //        retElemets.Add(bllEntity);
        //    }
        //    return retElemets;
        //}
    }
}
