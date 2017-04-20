using BLL.Entities;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping.Interfaces
{
    public interface IIndustrialObjectMapper
    {
        DalIndustrialObject MapToDal(BllIndustrialObject entity);
        BllIndustrialObject MapToBll(DalIndustrialObject entity);
    }
}
