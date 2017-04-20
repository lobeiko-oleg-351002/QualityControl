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
    public class CertificateLibMapper : ICertificateLibMapper
    {
        IUnitOfWork uow;
        public CertificateLibMapper(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public DalCertificateLib MapToDal(BllCertificateLib entity)
        {
            return new DalCertificateLib
            {
                Id = entity.Id
            };
        }

        public BllCertificateLib MapToBll(DalCertificateLib entity)
        {
            BllCertificateLib bllEntity = new BllCertificateLib
            {
                Id = entity.Id
            };         

            ISelectedCertificateMapper selectedCertificateMapper = new SelectedCertificateMapper(uow);

            foreach (var certificate in uow.SelectedCertificates.GetCertificatesByLibId(bllEntity.Id))
            {              
                var bllSelectedCertificate = selectedCertificateMapper.MapToBll(certificate);
                bllEntity.SelectedCertificate.Add(bllSelectedCertificate);
            }
            return bllEntity;
        }
    }
}
