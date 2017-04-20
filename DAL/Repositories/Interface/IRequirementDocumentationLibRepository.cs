using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IRequirementDocumentationLibRepository : IRepository<DalRequirementDocumentationLib>
    {
        new RequirementDocumentationLib Create(DalRequirementDocumentationLib entity);
        new void Delete(DalRequirementDocumentationLib entity);
        new DalRequirementDocumentationLib Get(int id);
        new IEnumerable<DalRequirementDocumentationLib> GetAll();
        new void Update(DalRequirementDocumentationLib entity);
    }
}
