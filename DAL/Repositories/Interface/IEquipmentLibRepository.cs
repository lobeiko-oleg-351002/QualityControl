using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IEquipmentLibRepository : IRepository<DalEquipmentLib>
    {
        new EquipmentLib Create(DalEquipmentLib entity);
        new void Delete(DalEquipmentLib entity);
        new DalEquipmentLib Get(int id);
        new IEnumerable<DalEquipmentLib> GetAll();
        new void Update(DalEquipmentLib entity);
    }
}
