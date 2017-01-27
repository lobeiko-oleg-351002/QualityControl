using DAL.Entities;
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
    }
}
