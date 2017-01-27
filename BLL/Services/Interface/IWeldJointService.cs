using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IWeldJointService : IService<BllWeldJoint>
    {
        BllWeldJoint GetWeldJointByName(string name);
    }
}
