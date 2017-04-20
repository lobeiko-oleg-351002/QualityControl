using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface ISelectedControlMethodDocumentationRepository : IRepository<DalSelectedControlMethodDocumentation>
    {
        IEnumerable<DalSelectedControlMethodDocumentation> GetControlMethodDocumentationsByLibId(int id);
        new SelectedControlMethodDocumentation Create(DalSelectedControlMethodDocumentation entity);
        new void Delete(DalSelectedControlMethodDocumentation entity);
        new DalSelectedControlMethodDocumentation Get(int id);
        new IEnumerable<DalSelectedControlMethodDocumentation> GetAll();
        new void Update(DalSelectedControlMethodDocumentation entity);
    }
}
