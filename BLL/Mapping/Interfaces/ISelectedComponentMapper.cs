using BLL.Entities;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping.Interfaces
{
    public interface ISelectedComponentMapper
    {
        DalSelectedComponent MapToDal(BllSelectedComponent entity);
        BllSelectedComponent MapToBll(DalSelectedComponent entity);
    }
}
