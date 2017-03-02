using AutoMapper;
using BLL.Entities;
using BLL.Services;
using BLL.Services.Interface;
using ServerWcfService.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities;
using UIL.Entities.Interface;

namespace ServerWcfService.Services
{
    public class CertificateRepository : Repository<UilCertificate, BllCertificate>, ICertificateRepository
    {
        private readonly ICertificateService certificateService;

        public CertificateRepository() : base(UiUnitOfWork.Instance.Certificates)
        {
            certificateService = UiUnitOfWork.Instance.Certificates;
        }

        public UilCertificate GetCertificateByTitle(string title)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllCertificate, UilCertificate>();
            });
            return Mapper.Map<UilCertificate>(certificateService.GetCertificateByTitle(title));
        }

        public override void Create(UilCertificate entity)
        {
            certificateService.Create(MapUilToBll(entity));
        }

        public override void Delete(UilCertificate entity)
        {
            certificateService.Delete(MapUilToBll(entity));
        }

        public override void Update(UilCertificate entity)
        {
            certificateService.Update(MapUilToBll(entity));
        }

        public override IEnumerable<UilCertificate> GetAll()
        {
            var bllEntities = certificateService.GetAll();
            var retElements = new List<UilCertificate>();
            foreach (var element in bllEntities)
            {
                retElements.Add(MapBllToUil(element));
            }
            return retElements;
        }

        public static UilCertificate MapBllToUil(BllCertificate bllEntity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllCertificate, UilCertificate>();
                cfg.CreateMap<BllControlName, UilControlName>();
                cfg.CreateMap<BllEmployee, UilEmployee>();
            });
            UilCertificate uilEntity = Mapper.Map<UilCertificate>(bllEntity);
            return uilEntity;
        }

        public static BllCertificate MapUilToBll(UilCertificate entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UilCertificate, BllCertificate>();
                cfg.CreateMap<UilEmployee, BllEmployee>();
                cfg.CreateMap<UilControlName, BllControlName>();
            });

            BllCertificate bllEntity = Mapper.Map<BllCertificate>(entity);
            return bllEntity;
        }

        public UilCertificate GetCertificateByEmployeeAndControlName(UilEmployee employee, UilControlName name)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UilControlName, BllControlName>();
            });
            return MapBllToUil(certificateService.GetCertificateByEmployeeAndControlName(EmployeeRepository.MapUilToBll(employee), Mapper.Map<BllControlName>(name)));
        }
    }
}
