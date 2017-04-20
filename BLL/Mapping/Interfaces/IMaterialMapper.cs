using BLL.Entities;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping.Interfaces
{
    public interface IMaterialMapper
    {
        DalMaterial MapToDal(BllMaterial entity);
        BllMaterial MapToBll(DalMaterial entity);
    }
}
