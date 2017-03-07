using AutoMapper;
using BLL.Entities;
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
        }

        public new BllCertificateLib Create(BllCertificateLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllSelectedCertificate, DalSelectedCertificate>();
                cfg.CreateMap<CertificateLib, DalCertificateLib>();
                cfg.CreateMap<BllCertificateLib, DalCertificateLib>();
                cfg.CreateMap<DalCertificateLib, BllCertificateLib>();
            });
            var ormEntity = uow.CertificateLibs.Create(Mapper.Map<DalCertificateLib>(entity));
            uow.Commit();
            var dalEntity = Mapper.Map<DalCertificateLib>(ormEntity);
            foreach (var Certificate in entity.SelectedCertificate)
            {
                Mapper.CreateMap<BllSelectedCertificate, DalSelectedCertificate>();
                var dalCertificate = Mapper.Map<DalSelectedCertificate>(Certificate);
                dalCertificate.CertificateLib_id = dalEntity.Id;
                uow.SelectedCertificates.Create(dalCertificate);
            }
            uow.Commit();
            Mapper.CreateMap<DalCertificateLib, BllCertificateLib>();
            return Mapper.Map<BllCertificateLib>(dalEntity);
        }

        public override BllCertificateLib Get(int id)
        {
            Mapper.CreateMap<DalCertificateLib, BllCertificateLib>();
            return MapDalToBll(uow.CertificateLibs.Get(id));
        }

        public override void Update(BllCertificateLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllSelectedCertificate, DalSelectedCertificate>();
                cfg.CreateMap<CertificateLib, DalCertificateLib>();
                cfg.CreateMap<BllCertificateLib, DalCertificateLib>();
                cfg.CreateMap<DalCertificateLib, BllCertificateLib>();
                cfg.CreateMap<BllCertificate, DalCertificate>();
            });
            foreach (var Certificate in entity.SelectedCertificate)
            {
                if (Certificate.Id == 0)
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<BllSelectedCertificate, DalSelectedCertificate>();
                        cfg.CreateMap<BllCertificate, DalCertificate>();
                    });
                    var dalCertificate = Mapper.Map<DalSelectedCertificate>(Certificate);
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

        private BllCertificateLib MapDalToBll(DalCertificateLib dalEntity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DalCertificateLib, BllCertificateLib>();
                cfg.CreateMap<DalCertificate, BllCertificate>();
                cfg.CreateMap<DalSelectedCertificate, BllSelectedCertificate>();
                cfg.CreateMap<DalControlName, BllControlName>();
            });
            BllCertificateLib bllEntity = Mapper.Map<BllCertificateLib>(dalEntity);

            SelectedCertificateService selectedCertificateService = new SelectedCertificateService(uow);
            CertificateService certificateService = new CertificateService(uow);
            foreach (var certificate in uow.SelectedCertificates.GetCertificatesByLibId(dalEntity.Id))
            {
                var bllCertificate = certificate.Certificate_id != null ? certificateService.Get((int)certificate.Certificate_id) : null;
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<DalCertificate, BllCertificate>();
                    cfg.CreateMap<DalSelectedCertificate, BllSelectedCertificate>();
                    cfg.CreateMap<DalControlName, BllControlName>();
                });
                var bllSelectedCertificate = Mapper.Map<BllSelectedCertificate>(certificate);
                bllSelectedCertificate.Certificate = bllCertificate;
                bllEntity.SelectedCertificate.Add(bllSelectedCertificate);
                
            }
            return bllEntity;
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
