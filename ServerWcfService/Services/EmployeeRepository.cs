using AutoMapper;
using BLL.Entities;
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
    public class EmployeeRepository : Repository<UilEmployee,BllEmployee>, IEmployeeRepository
    {
        private readonly IEmployeeService EmployeeService;

        public EmployeeRepository() : base(UiUnitOfWork.Instance.Employees)
        {
            EmployeeService = UiUnitOfWork.Instance.Employees;
        }

        public override void Create(UilEmployee entity)
        {
            EmployeeService.Create(MapUilToBll(entity));
        }

        public override void Delete(UilEmployee entity)
        {
            EmployeeService.Delete(MapUilToBll(entity));
        }

        public override void Update(UilEmployee entity)
        {
            EmployeeService.Update(MapUilToBll(entity));
        }

        public override IEnumerable<UilEmployee> GetAll()
        {
            var bllEntities = EmployeeService.GetAll();
            var retElements = new List<UilEmployee>();
            foreach (var element in bllEntities)
            {
                retElements.Add(MapBllToUil(element));
            }
            return retElements;
        }

        public static UilEmployee MapBllToUil(BllEmployee bllEntity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllEmployee, UilEmployee>();
                cfg.CreateMap<BllCertificateLib, UilCertificateLib>();
                cfg.CreateMap<BllSelectedCertificate, UilSelectedCertificate>();
                cfg.CreateMap<BllCertificate, UilCertificate>();
                cfg.CreateMap<BllControlName, UilControlName>();
            });
            UilEmployee uilEntity = Mapper.Map<UilEmployee>(bllEntity);
            //uilEntity.Certificate = bllEntity.Certificate != null ? CertificateRepository.MapBllToUil(bllEntity.Template) : null;
            return uilEntity;
        }

        public static BllEmployee MapUilToBll(UilEmployee entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UilEmployee, BllEmployee>();
                cfg.CreateMap<UilCertificateLib, BllCertificateLib>();
                cfg.CreateMap<UilSelectedCertificate, BllSelectedCertificate>();
                cfg.CreateMap<UilCertificate, BllCertificate>();
                cfg.CreateMap<UilControlName, BllControlName>();
            });

            BllEmployee bllEntity = Mapper.Map<BllEmployee>(entity);
           // bllEntity.Template = entity.Template != null ? TemplateRepository.MapUilToBll(entity.Template) : null;

            return bllEntity;
        }
    }
}
