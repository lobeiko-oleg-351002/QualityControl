using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IJournalRepository : IRepository<DalJournal>
    {
        new Journal Create(DalJournal entity);
        new void Delete(DalJournal entity);
        new DalJournal Get(int id);
        new IEnumerable<DalJournal> GetAll();
        new void Update(DalJournal entity);
    }
}
