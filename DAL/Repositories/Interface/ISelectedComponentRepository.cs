using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public  interface ISelectedComponentRepository : IRepository<DalSelectedComponent>
    {
        IEnumerable<DalSelectedComponent> GetComponentsByLibId(int id);
    }
}
