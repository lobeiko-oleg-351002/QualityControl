using AutoMapper;
using DAL.Entities;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CertificateRepository : Repository<DalCertificate, Certificate>, ICertificateRepository
    {
        private readonly ServiceDB context;
        public CertificateRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalCertificate GetCertificateByTitle(string title)
        {
            Mapper.CreateMap<Certificate, DalCertificate>();
            var ormEntity = context.Certificates.FirstOrDefault(entity => entity.title == title);
            return Mapper.Map<DalCertificate>(ormEntity);
        }




    }
}
