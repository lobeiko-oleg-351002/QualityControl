using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IControlMethodsLibRepository : IRepository<DalControlMethodsLib>
    {
        new ControlMethodsLib Create(DalControlMethodsLib entity);
        new void Delete(DalControlMethodsLib entity);
        new DalControlMethodsLib Get(int id);
        new IEnumerable<DalControlMethodsLib> GetAll();
        new void Update(DalControlMethodsLib entity);
    }
}
