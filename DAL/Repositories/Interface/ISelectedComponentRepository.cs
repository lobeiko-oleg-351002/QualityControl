using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public  interface ISelectedComponentRepository : IRepository<DalSelectedComponent>
    {
        IEnumerable<DalSelectedComponent> GetComponentsByLibId(int id);
        new SelectedComponent Create(DalSelectedComponent entity);
        new void Delete(DalSelectedComponent entity);
        new DalSelectedComponent Get(int id);
        new IEnumerable<DalSelectedComponent> GetAll();
        new void Update(DalSelectedComponent entity);
    }
}
