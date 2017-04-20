using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;

namespace DAL.Repositories.Interface
{
    public interface IRequirementDocumentationRepository : IRepository<DalRequirementDocumentation>
    {
        new RequirementDocumentation Create(DalRequirementDocumentation entity);
        new void Delete(DalRequirementDocumentation entity);
        new DalRequirementDocumentation Get(int id);
        new IEnumerable<DalRequirementDocumentation> GetAll();
        new void Update(DalRequirementDocumentation entity);

        DalRequirementDocumentation GetRequirementDocumentationByName(string name);
    }
}
