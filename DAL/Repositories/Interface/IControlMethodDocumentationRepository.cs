using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IControlMethodDocumentationRepository : IRepository<DalControlMethodDocumentation>
    {
        DalControlMethodDocumentation GetControlMethodDocumentationByName(string name);
        new ControlMethodDocumentation Create(DalControlMethodDocumentation entity);
        new void Delete(DalControlMethodDocumentation entity);
        new DalControlMethodDocumentation Get(int id);
        new IEnumerable<DalControlMethodDocumentation> GetAll();
        new void Update(DalControlMethodDocumentation entity);
    }
}
