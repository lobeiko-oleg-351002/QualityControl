using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IEquipmentRepository : IRepository<DalEquipment>
    {
        DalEquipment GetEquipmentByName(string name);
        IEnumerable<DalEquipment> GetEquipmentByType(string type);
        IEnumerable<DalEquipment> GetEquipmentByFactoryNumber(int number);
        IEnumerable<DalEquipment> GetCheckedEquipment();
        IEnumerable<DalEquipment> GetUncheckedEquipment();

        new Equipment Create(DalEquipment entity);
        new void Delete(DalEquipment entity);
        new DalEquipment Get(int id);
        new IEnumerable<DalEquipment> GetAll();
        new void Update(DalEquipment entity);
    }
}
