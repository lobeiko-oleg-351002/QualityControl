using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
using BLL.Mapping.Interfaces;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CertificateLibService : Service<BllCertificateLib, DalCertificateLib>, ICertificateLibService
    {
        private readonly IUnitOfWork uow;

        public CertificateLibService(IUnitOfWork uow) : base(uow, uow.CertificateLibs)
        {
            this.uow = uow;
            bllMapper = new CertificateLibMapper(uow);

        }

        CertificateLibMapper bllMapper;

        public new BllCertificateLib Create(BllCertificateLib entity)
        {
            var ormEntity = uow.CertificateLibs.Create(bllMapper.MapToDal(entity));
            uow.Commit();
            entity.Id = ormEntity.id;
            ISelectedCertificateMapper selectedCertificateMapper = new SelectedCertificateMapper(uow);
            foreach (var Certificate in entity.SelectedCertificate)
            {
                var dalCertificate = selectedCertificateMapper.MapToDal(Certificate);
                dalCertificate.CertificateLib_id = entity.Id;
                var ormCertificate = uow.SelectedCertificates.Create(dalCertificate);
                uow.Commit();
                Certificate.Id = ormCertificate.id;
            }

            return entity;
        }

        public override BllCertificateLib Get(int id)
        {
            return bllMapper.MapToBll(uow.CertificateLibs.Get(id));
        }

        public override void Update(BllCertificateLib entity)
        {
            ISelectedCertificateMapper selectedCertificateMapper = new SelectedCertificateMapper(uow);
            foreach (var Certificate in entity.SelectedCertificate)
            {
                if (Certificate.Id == 0)
                {
                    var dalCertificate = selectedCertificateMapper.MapToDal(Certificate);
                    dalCertificate.CertificateLib_id = entity.Id;
                    uow.SelectedCertificates.Create(dalCertificate);
                }
            }
            var certificatesWithLibId = uow.SelectedCertificates.GetCertificatesByLibId(entity.Id);
            foreach (var certificate in certificatesWithLibId)
            {
                bool isTrashCertificate = true;
                foreach (var selectedCertificate in entity.SelectedCertificate)
                {
                    if (certificate.Id == selectedCertificate.Id)
                    {
                        isTrashCertificate = false;
                        break;
                    }
                }
                if (isTrashCertificate == true)
                {
                    uow.SelectedCertificates.Delete(certificate);
                }
            }
            uow.Commit();
        }

        //private DalCertificateLib MapBllToDal(BllCertificateLib entity)
        //{
        //    Mapper.Initialize(cfg =>
        //    {
        //        cfg.CreateMap<BllCertificateLib, DalCertificateLib>();
        //        cfg.CreateMap<BllCertificate, DalCertificate>();
        //        cfg.CreateMap<BllSelectedCertificate, DalSelectedCertificate>();
        //    });
        //    DalCertificateLib dalEntity = Mapper.Map<DalCertificateLib>(entity);
        //}
    }
}
