using BLL.Entities;
using BLL.Services.Interface;
using ServerWcfService.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities;

namespace ServerWcfService.Services
{
    public class ImageRepository : Repository<UilImage, BllImage>, IImageRepository
    {
        private readonly IImageService ImageService;

        public ImageRepository() : base(UiUnitOfWork.Instance.Images)
        {
            ImageService = UiUnitOfWork.Instance.Images;
        }
    }
}
