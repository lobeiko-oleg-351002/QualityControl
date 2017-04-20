using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IComponentRepository : IRepository<DalComponent>
    {
        DalComponent GetComponentByName(string name);
        IEnumerable<DalComponent> GetComponentsByTemplateId(int id);
        new Component Create(DalComponent entity);
        new void Delete(DalComponent entity);
        new DalComponent Get(int id);
        new IEnumerable<DalComponent> GetAll();
        new void Update(DalComponent entity);
    }
}
