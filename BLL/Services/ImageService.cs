using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
using BLL.Mapping.Interfaces;
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
            bllMapper = new ImageMapper();
        }
        IImageMapper bllMapper;
        public override void Create(BllImage entity)
        {
            uow.Images.Create(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Update(BllImage entity)
        {
            uow.Images.Update(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllImage entity)
        {
            uow.Images.Delete(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override IEnumerable<BllImage> GetAll()
        {
            var elements = uow.Images.GetAll();
            var retElemets = new List<BllImage>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }

        public override BllImage Get(int id)
        {
            return bllMapper.MapToBll(uow.Images.Get(id));
        }
    }
}
