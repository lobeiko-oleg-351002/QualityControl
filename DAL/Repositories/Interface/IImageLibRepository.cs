using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IImageLibRepository : IRepository<DalImageLib>
    {
        new ImageLib Create(DalImageLib entity);
        new void Delete(DalImageLib entity);
        new DalImageLib Get(int id);
        new IEnumerable<DalImageLib> GetAll();
        new void Update(DalImageLib entity);
    }
}
