using AutoMapper;
using DAL.Entities;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ImageLibRepository : Repository<DalImageLib, ImageLib>, IImageLibRepository
    {
        private readonly ServiceDB context;
        public ImageLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public new ImageLib Create(DalImageLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DalImageLib, ImageLib>();
                cfg.CreateMap<ImageLib, DalImageLib>();
            });
            var res = context.Set<ImageLib>().Add(Mapper.Map<ImageLib>(entity));
            return res;
        }

        //public List<DalImage> GetImagesFromLib(DalImageLib lib)
        //{
        //    Mapper.CreateMap<Image, DalImage>();
        //    var ormLib = context.ImageLibs.FirstOrDefault(entity => entity.id == lib.Id);
        //    List<DalImage> result = new List<DalImage>();
        //    foreach(Image image in ormLib.Image)
        //    {
        //        result.Add(Mapper.Map<DalImage>(image));
        //    }
        //    return result;
        //}
    }
}
