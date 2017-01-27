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
    }
}
