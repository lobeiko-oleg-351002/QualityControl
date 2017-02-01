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
    public class ImageLibService : Service<BllImageLib, DalImageLib>, IImageLibService
    {
        private readonly IUnitOfWork uow;

        public ImageLibService(IUnitOfWork uow) : base(uow, uow.ImageLibs)
        {
            this.uow = uow;
        }

        public new BllImageLib Create(BllImageLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllImage, DalImage>();
                cfg.CreateMap<ImageLib, DalImageLib>();
                cfg.CreateMap<BllImageLib, DalImageLib>();
                cfg.CreateMap<DalImageLib, BllImageLib>();
            });
            var ormEntity = uow.ImageLibs.Create(Mapper.Map<DalImageLib>(entity));
            uow.Commit();
            entity.Id = ormEntity.id;
            foreach (var image in entity.Image)
            {
                Mapper.CreateMap<BllImage, DalImage>();   
                var dalImage = Mapper.Map<DalImage>(image);
                dalImage.Image_lib_id = entity.Id;
                var ormImage =  uow.Images.Create(dalImage);
                uow.Commit();
                image.Id = ormImage.id;
            }
            return entity;
        }

        public override BllImageLib Get(int id)
        {
            Mapper.CreateMap<DalImageLib, BllImageLib>();
            var retElement = Mapper.Map<BllImageLib>(uow.ImageLibs.Get(id));
            var images = uow.Images.GetImagesByLibId(retElement.Id);
            foreach (var image in images)
            {
                Mapper.CreateMap<DalImage, BllImage>();
                retElement.Image.Add(Mapper.Map <BllImage>(image));
            }
            
            return retElement;
        }

        public new BllImageLib Update(BllImageLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllImage, DalImage>();
                cfg.CreateMap<ImageLib, DalImageLib>();
                cfg.CreateMap<BllImageLib, DalImageLib>();
                cfg.CreateMap<DalImageLib, BllImageLib>();
            });
            foreach (var image in entity.Image)
            {
                Mapper.CreateMap<BllImage, DalImage>();
                if (image.Id == 0)
                {
                    var dalImage = Mapper.Map<DalImage>(image);
                    dalImage.Image_lib_id = entity.Id;
                    var ormImage = uow.Images.Create(dalImage);
                    uow.Commit();
                    image.Id = ormImage.id;
                }
               
            }
            //uow.Commit();

            var ImagesWithLibId = uow.Images.GetImagesByLibId(entity.Id);
            foreach (var Image in ImagesWithLibId)
            {
                bool isTrashImage = true;
                foreach (var image in entity.Image)
                {
                    if (Image.Id == Image.Id)
                    {
                        isTrashImage = false;
                        break;
                    }
                }
                if (isTrashImage == true)
                {
                    uow.Images.Delete(Image);
                }
            }
            uow.Commit();

            return entity;
        }
    }
}
