using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IMaterialRepository : IRepository<DalMaterial>
    {
        DalMaterial GetMaterialByName(string name);
    }
}
