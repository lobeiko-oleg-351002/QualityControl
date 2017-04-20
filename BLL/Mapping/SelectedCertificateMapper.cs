using BLL.Entities;
using BLL.Mapping.Interfaces;
using BLL.Services;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class SelectedCertificateMapper : ISelectedCertificateMapper
    {
        IUnitOfWork uow;
        public SelectedCertificateMapper(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public DalSelectedCertificate MapToDal(BllSelectedCertificate entity)
        {
            DalSelectedCertificate dalEntity = new DalSelectedCertificate
            {
                Id = entity.Id,
                Certificate_id = entity.Certificate.Id,
            };

            return dalEntity;
        }

        public BllSelectedCertificate MapToBll(DalSelectedCertificate entity)
        {
            CertificateService certificateService = new CertificateService(uow);
            var bllCertificate = entity.Certificate_id != null ? certificateService.Get((int)entity.Certificate_id) : null;

            BllSelectedCertificate bllEntity = new BllSelectedCertificate
            {
                Id = entity.Id,
                Certificate = bllCertificate
            };

            return bllEntity;
        }
    }
}
