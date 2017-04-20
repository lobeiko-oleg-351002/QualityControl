using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IImageRepository : IRepository<DalImage>
    {
        IEnumerable<DalImage> GetImagesByLibId(int id);
        new Image Create(DalImage entity);
        new void Delete(DalImage entity);
        new DalImage Get(int id);
        new IEnumerable<DalImage> GetAll();
        new void Update(DalImage entity);
    }
}
