using BLL.Entities;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping.Interfaces
{
    public interface ISelectedControlMethodDocumentationMapper
    {
        DalSelectedControlMethodDocumentation MapToDal(BllSelectedControlMethodDocumentation entity);
        BllSelectedControlMethodDocumentation MapToBll(DalSelectedControlMethodDocumentation entity);
    }
}
