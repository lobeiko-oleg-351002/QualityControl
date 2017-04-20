using DAL.Entities;
using ORM;
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
        new SelectedCertificate Create(DalSelectedCertificate entity);
        new void Delete(DalSelectedCertificate entity);
        new DalSelectedCertificate Get(int id);
        new IEnumerable<DalSelectedCertificate> GetAll();
        new void Update(DalSelectedCertificate entity);
    }
}
