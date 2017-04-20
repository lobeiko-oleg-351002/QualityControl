using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IIndustrialObjectRepository : IRepository<DalIndustrialObject>
    {
        DalIndustrialObject GetIndustrialObjectByName(string name);
        new IndustrialObject Create(DalIndustrialObject entity);
        new void Delete(DalIndustrialObject entity);
        new DalIndustrialObject Get(int id);
        new IEnumerable<DalIndustrialObject> GetAll();
        new void Update(DalIndustrialObject entity);
    }
}
