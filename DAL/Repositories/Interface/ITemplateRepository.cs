using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface ITemplateRepository : IRepository<DalTemplate>
    {
        DalTemplate GetTemplateByName(string name);
        new Template Create(DalTemplate entity);
        new void Delete(DalTemplate entity);
        new DalTemplate Get(int id);
        new IEnumerable<DalTemplate> GetAll();
        new void Update(DalTemplate entity);
    }
}
