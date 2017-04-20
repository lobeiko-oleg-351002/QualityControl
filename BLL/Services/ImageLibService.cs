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
    public class ImageLibService : Service<BllImageLib, DalImageLib>, IImageLibService
    {
        private readonly IUnitOfWork uow;
        IImageLibMapper bllMapper;
        public ImageLibService(IUnitOfWork uow) : base(uow, uow.ImageLibs)
        {
            this.uow = uow;
            bllMapper = new ImageLibMapper(uow);
        }

        public new BllImageLib Create(BllImageLib entity)
        {
            var ormEntity = uow.ImageLibs.Create(bllMapper.MapToDal(entity));
            uow.Commit();
            entity.Id = ormEntity.id;
            ImageMapper imageMapper = new ImageMapper();
            foreach (var image in entity.Image)
            {
                var dalImage = imageMapper.MapToDal(image);
                dalImage.ImageLib_id = entity.Id;
                var ormImage =  uow.Images.Create(dalImage);
                uow.Commit();
                image.Id = ormImage.id;
            }
            return entity;
        }

        public override BllImageLib Get(int id)
        {
            var retElement = bllMapper.MapToBll(uow.ImageLibs.Get(id));
            var images = uow.Images.GetImagesByLibId(retElement.Id);
            ImageMapper imageMapper = new ImageMapper();
            foreach (var image in images)
            {
                retElement.Image.Add(imageMapper.MapToBll(image));
            }
            
            return retElement;
        }

        public new BllImageLib Update(BllImageLib entity)
        {
            ImageMapper imageMapper = new ImageMapper();
            foreach (var image in entity.Image)
            {
                if (image.Id == 0)
                {
                    var dalImage = imageMapper.MapToDal(image);
                    dalImage.ImageLib_id = entity.Id;
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
