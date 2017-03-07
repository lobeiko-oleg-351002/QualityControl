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
    public class ImageRepository : Repository<DalImage, Image>, IImageRepository
    {
        private readonly ServiceDB context;
        public ImageRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public new Image Create(DalImage entity)
        {
            Mapper.CreateMap<DalImage, Image>();
            var ormEntity = Mapper.Map<Image>(entity);
            ormEntity.ImageLib = context.ImageLibs.FirstOrDefault(e => e.id == ormEntity.imageLib_id);
            //ormEntity.ImageLib.Image.Add(ormEntity);
            return context.Set<Image>().Add(Mapper.Map<Image>(entity));
        }
        public IEnumerable<DalImage> GetImagesByLibId(int id)
        {
            Mapper.CreateMap<Image, DalImage>();
            var elements = context.Set<Image>().Where(entity => entity.imageLib_id == id);
            var retElemets = new List<DalImage>(); 
            foreach (var element in elements)
            {
                Mapper.CreateMap<Image, DalImage>();
                retElemets.Add(Mapper.Map<DalImage>(element));
            }
            return retElemets;
        }
    }
}
