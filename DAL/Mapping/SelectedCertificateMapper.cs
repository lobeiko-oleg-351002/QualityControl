using DAL.Entities;
using DAL.Mapping.Interfaces;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping
{
    public class SelectedCertificateMapper : ISelectedCertificateMapper
    {
        public DalSelectedCertificate MapToDal(SelectedCertificate entity)
        {
            return new DalSelectedCertificate
            {
                Id = entity.id,
                CertificateLib_id = entity.certificateLib_id,
                Certificate_id = entity.certificate_id
            };
        }

        public SelectedCertificate MapToOrm(DalSelectedCertificate entity)
        {
            return new SelectedCertificate
            {
                id = entity.Id,
                certificate_id = entity.Certificate_id,
                certificateLib_id = entity.CertificateLib_id
            };
        }
    }
}
