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
    }
}
