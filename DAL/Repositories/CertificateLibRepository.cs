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
    public class CertificateLibRepository : Repository<DalCertificateLib, CertificateLib>, ICertificateLibRepository
    {
        private readonly ServiceDB context;
        public CertificateLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public new CertificateLib Create(DalCertificateLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DalCertificateLib, CertificateLib>();
                cfg.CreateMap<CertificateLib, DalCertificateLib>();
            });
            var res = context.Set<CertificateLib>().Add(Mapper.Map<CertificateLib>(entity));
            return res;
        }
    }
}
