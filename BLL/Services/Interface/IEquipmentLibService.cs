using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IEquipmentLibService : IService<BllEquipmentLib>
    {
        new BllEquipmentLib Create(BllEquipmentLib entity);
    }
}
