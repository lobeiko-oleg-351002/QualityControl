using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IResultRepository : IRepository<DalResult>
    {
        DalResult GetResultByNumber(int number);
        IEnumerable<DalResult> GetResultsByLibId(int id);
        new Result Create(DalResult entity);
        new void Delete(DalResult entity);
        new DalResult Get(int id);
        new IEnumerable<DalResult> GetAll();
        new void Update(DalResult entity);
    }
}
