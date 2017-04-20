using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface ICertificateRepository : IRepository<DalCertificate>
    {
        DalCertificate GetCertificateByTitle(string title);
        DalCertificate GetCertificateByEmployeeIdAndControlId(int e_id, int c_id);
        new Certificate Create(DalCertificate entity);
        new void Delete(DalCertificate entity);
        new DalCertificate Get(int id);
        new IEnumerable<DalCertificate> GetAll();
        new void Update(DalCertificate entity);
    }
}
