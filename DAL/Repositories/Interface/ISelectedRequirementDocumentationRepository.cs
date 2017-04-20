using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface ISelectedRequirementDocumentationRepository : IRepository<DalSelectedRequirementDocumentation>
    {
        IEnumerable<DalSelectedRequirementDocumentation> GetRequirementDocumentationsByLibId(int id);
        new SelectedRequirementDocumentation Create(DalSelectedRequirementDocumentation entity);
        new void Delete(DalSelectedRequirementDocumentation entity);
        new DalSelectedRequirementDocumentation Get(int id);
        new IEnumerable<DalSelectedRequirementDocumentation> GetAll();
        new void Update(DalSelectedRequirementDocumentation entity);
    }
}
