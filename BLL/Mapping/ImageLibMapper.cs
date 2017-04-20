using BLL.Entities;
using BLL.Mapping.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class ImageLibMapper : IImageLibMapper
    {
        IUnitOfWork uow;
        public ImageLibMapper(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public DalImageLib MapToDal(BllImageLib entity)
        {
            return new DalImageLib
            {
                Id = entity.Id
            };
        }

        public BllImageLib MapToBll(DalImageLib entity)
        {
            BllImageLib bllEntity = new BllImageLib
            {
                Id = entity.Id
            };

            IImageMapper imageMapper = new ImageMapper();

            foreach (var Image in uow.Images.GetImagesByLibId(bllEntity.Id))
            {
                var bllImage = imageMapper.MapToBll(Image);
                bllEntity.Image.Add(bllImage);
            }
            return bllEntity;
        }
    }
}
