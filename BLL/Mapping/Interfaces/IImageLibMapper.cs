using BLL.Entities;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping.Interfaces
{
    public interface IImageLibMapper
    {
        DalImageLib MapToDal(BllImageLib entity);
        BllImageLib MapToBll(DalImageLib entity);
    }
}
