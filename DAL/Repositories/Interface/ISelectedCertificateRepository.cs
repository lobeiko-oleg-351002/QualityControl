using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface ISelectedCertificateRepository : IRepository<DalSelectedCertificate>
    {
        IEnumerable<DalSelectedCertificate> GetCertificatesByLibId(int id);
    }
}
