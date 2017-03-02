using AutoMapper;
using BLL.Entities;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CertificateService : Service<BllCertificate, DalCertificate>, ICertificateService
    {
        private readonly IUnitOfWork uow;

        public CertificateService(IUnitOfWork uow) : base(uow, uow.Certificates)
        {
            this.uow = uow;
        }
        public BllCertificate GetCertificateByTitle(string title)
        {
            Mapper.CreateMap<DalCertificate, BllCertificate>();
            return Mapper.Map<BllCertificate>(uow.Certificates.GetCertificateByTitle(title));
        }

        public override void Create(BllCertificate entity)
        {
            uow.Certificates.Create(MapBllToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllCertificate entity)
        {
            uow.Certificates.Delete(MapBllToDal(entity));
            uow.Commit();
        }

        public override IEnumerable<BllCertificate> GetAll()
        {
            var elements = uow.Certificates.GetAll();
            var retElemets = new List<BllCertificate>();
            foreach (var element in elements)
            {
                retElemets.Add(MapDalToBll(element));
            }
            return retElemets;
        }

        public override BllCertificate Get(int id)
        {
            return MapDalToBll(uow.Certificates.Get(id));
        }

        private DalCertificate MapBllToDal(BllCertificate entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllCertificate, DalCertificate>();
            });

            DalCertificate dalEntity = Mapper.Map<DalCertificate>(entity);
            dalEntity.ControlName_id = entity.ControlName != null ? entity.ControlName.Id : (int?)null;
            return dalEntity;
        }

        private BllCertificate MapDalToBll(DalCertificate entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DalCertificate, BllCertificate>();
            });
            BllCertificate bllCertificate = Mapper.Map<BllCertificate>(entity);
            ControlNameService ControlNameService = new ControlNameService(uow);
            EmployeeService employeeService = new EmployeeService(uow);
            bllCertificate.ControlName = entity.ControlName_id != null ? ControlNameService.Get((int)entity.ControlName_id) : null;
            bllCertificate.Employee = entity.Employee_id != null ? employeeService.Get((int)entity.Employee_id) : null;
            return bllCertificate;
        }

        public BllCertificate GetCertificateByEmployeeAndControlName(BllEmployee employee, BllControlName name)
        {
            Mapper.CreateMap<DalCertificate, BllCertificate>();
            return Mapper.Map<BllCertificate>(uow.Certificates.GetCertificateByEmployeeIdAndControlId(employee.Id, name.Id));
        }
    }
}
