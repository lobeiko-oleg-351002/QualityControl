using AutoMapper;
using DAL.Entities;
using DAL.Mapping;
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

        ImageLibMapper mapper = new ImageLibMapper();

        public new void Delete(DalImageLib entity)
        {
            var ormEntity = context.Set<ImageLib>().Single(ImageLib => ImageLib.id == entity.Id);
            context.Set<ImageLib>().Remove(ormEntity);
        }

        public new DalImageLib Get(int id)
        {
            var ormEntity = context.Set<ImageLib>().FirstOrDefault(ImageLib => ImageLib.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalImageLib> GetAll()
        {
            var elements = context.Set<ImageLib>().Select(ImageLib => ImageLib);
            var retElemets = new List<DalImageLib>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalImageLib entity)
        {
            var ormEntity = context.Set<ImageLib>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new ImageLib Create(DalImageLib entity)
        {
            var res = context.Set<ImageLib>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
