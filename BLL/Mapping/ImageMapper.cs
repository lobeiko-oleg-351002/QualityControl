using BLL.Entities;
using BLL.Mapping.Interfaces;
using BLL.Services;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class ImageMapper : IImageMapper
    {

        public ImageMapper()
        {

        }

        public DalImage MapToDal(BllImage entity)
        {
            DalImage dalEntity = new DalImage
            {
                Id = entity.Id,
                Image = entity.Image
            };

            return dalEntity;
        }

        public BllImage MapToBll(DalImage entity)
        {
            BllImage bllEntity = new BllImage
            {
                Id = entity.Id,
                Image = entity.Image
            };

            return bllEntity;
        }
    }
}
