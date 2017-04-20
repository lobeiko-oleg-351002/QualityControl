using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
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
            bllMapper = new CertificateMapper(uow);
        }

        CertificateMapper bllMapper;

        public BllCertificate GetCertificateByTitle(string title)
        {
            return bllMapper.MapToBll(uow.Certificates.GetCertificateByTitle(title));
        }

        public override void Create(BllCertificate entity)
        {
            uow.Certificates.Create(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Update(BllCertificate entity)
        {
            uow.Certificates.Update(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllCertificate entity)
        {
            uow.Certificates.Delete(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override IEnumerable<BllCertificate> GetAll()
        {
            var elements = uow.Certificates.GetAll();
            var retElemets = new List<BllCertificate>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }

        public override BllCertificate Get(int id)
        {
            return bllMapper.MapToBll(uow.Certificates.Get(id));
        }

        public BllCertificate GetCertificateByEmployeeAndControlName(BllEmployee employee, BllControlName name)
        {
            return bllMapper.MapToBll(uow.Certificates.GetCertificateByEmployeeIdAndControlId(employee.Id, name.Id));
        }
    }
}
