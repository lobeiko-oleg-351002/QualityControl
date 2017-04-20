using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IWeldJointRepository : IRepository<DalWeldJoint>
    {
        DalWeldJoint GetWeldJointByName(string name);
        new WeldJoint Create(DalWeldJoint entity);
        new void Delete(DalWeldJoint entity);
        new DalWeldJoint Get(int id);
        new IEnumerable<DalWeldJoint> GetAll();
        new void Update(DalWeldJoint entity);
    }
}
