using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping.Interfaces
{
    public interface IRoleMapper
    {
        DalRole MapToDal(Role entity);
        Role MapToOrm(DalRole entity);
    }
}
