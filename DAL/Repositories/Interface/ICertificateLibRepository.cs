using DAL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface ICertificateLibRepository : IRepository<DalCertificateLib>
    {
        new CertificateLib Create(DalCertificateLib entity);
        new void Delete(DalCertificateLib entity);
        new DalCertificateLib Get(int id);
        new IEnumerable<DalCertificateLib> GetAll();
        new void Update(DalCertificateLib entity);
    }
}
