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
    public class ImageRepository : Repository<DalImage, Image>, IImageRepository
    {
        private readonly ServiceDB context;
        public ImageRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public new Image Create(DalImage entity)
        {
            var ormEntity = mapper.MapToOrm(entity);
            ormEntity.ImageLib = context.ImageLibs.FirstOrDefault(e => e.id == ormEntity.imageLib_id);
            return context.Set<Image>().Add(ormEntity);
        }
        public IEnumerable<DalImage> GetImagesByLibId(int id)
        {
            ImageMapper mapper = new ImageMapper();
            var elements = context.Set<Image>().Where(entity => entity.imageLib_id == id);
            var retElemets = new List<DalImage>(); 
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapToDal(element));
            }
            return retElemets;
        }

        ImageMapper mapper = new ImageMapper();

        public new void Delete(DalImage entity)
        {
            var ormEntity = context.Set<Image>().Single(Image => Image.id == entity.Id);
            context.Set<Image>().Remove(ormEntity);
        }

        public new DalImage Get(int id)
        {
            var ormEntity = context.Set<Image>().FirstOrDefault(Image => Image.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalImage> GetAll()
        {
            var elements = context.Set<Image>().Select(Image => Image);
            var retElemets = new List<DalImage>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalImage entity)
        {
            var ormEntity = context.Set<Image>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

    }
}
