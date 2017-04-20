using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IMaterialRepository : IRepository<DalMaterial>
    {
        DalMaterial GetMaterialByName(string name);
        new Material Create(DalMaterial entity);
        new void Delete(DalMaterial entity);
        new DalMaterial Get(int id);
        new IEnumerable<DalMaterial> GetAll();
        new void Update(DalMaterial entity);
    }
}
