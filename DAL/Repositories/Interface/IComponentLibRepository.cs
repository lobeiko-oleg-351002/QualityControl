using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IComponentLibRepository : IRepository<DalComponentLib>
    {
        new ComponentLib Create(DalComponentLib entity);
    }
}
