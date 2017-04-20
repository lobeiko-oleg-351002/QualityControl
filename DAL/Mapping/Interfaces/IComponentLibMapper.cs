using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping.Interfaces
{
    public interface IComponentLibMapper
    {
        DalComponentLib MapToDal(ComponentLib entity);
        ComponentLib MapToOrm(DalComponentLib entity);
    }
}
