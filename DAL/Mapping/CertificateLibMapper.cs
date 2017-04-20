using DAL.Mapping.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using ORM;

namespace DAL.Mapping
{
    public class CertificateLibMapper : ICertificateLibMapper
    {
        public DalCertificateLib MapToDal(CertificateLib entity)
        {
            return new DalCertificateLib
            {
                Id = entity.id,
            };
        }

        public CertificateLib MapToOrm(DalCertificateLib entity)
        {
            return new CertificateLib
            {
                id = entity.Id
            };
        }
    }
}
