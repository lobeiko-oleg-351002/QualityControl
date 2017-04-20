using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping.Interfaces
{
    public interface ISelectedComponentMapper
    {
        DalSelectedComponent MapToDal(SelectedComponent entity);
        SelectedComponent MapToOrm(DalSelectedComponent entity);
    }
}
