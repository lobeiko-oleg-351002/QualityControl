using DAL.Entities;
using DAL.Mapping.Interfaces;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping
{
    public class ImageLibMapper : IImageLibMapper
    {
        public DalImageLib MapToDal(ImageLib entity)
        {
            return new DalImageLib
            {
                Id = entity.id,
            };
        }

        public ImageLib MapToOrm(DalImageLib entity)
        {
            return new ImageLib
            {
                id = entity.Id
            };
        }
    }
}
