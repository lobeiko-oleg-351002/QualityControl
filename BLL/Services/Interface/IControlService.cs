using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IControlService : IService<BllControl>
    {
        IEnumerable<BllControl> GetAllControlled();
        IEnumerable<BllControl> GetAllUncontrolled();
        BllControl GetControlByNumber(int number);
        new BllControl Create(BllControl entity);
    }
}
