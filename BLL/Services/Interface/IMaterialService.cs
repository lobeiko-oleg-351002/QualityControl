using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IMaterialService : IService<BllMaterial>
    {
        BllMaterial GetMaterialByName(string name);
    }
}
