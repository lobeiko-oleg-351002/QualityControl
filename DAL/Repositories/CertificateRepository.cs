using AutoMapper;
using DAL.Entities;
using DAL.Mapping;
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

        CertificateMapper mapper = new CertificateMapper();

        public DalCertificate GetCertificateByEmployeeIdAndControlId(int e_id, int c_id)
        {
            var ormEntity = context.Certificates.FirstOrDefault(entity => entity.employee_id == e_id && entity.controlName_id == c_id);
            return mapper.MapToDal(ormEntity);
        }

        public DalCertificate GetCertificateByTitle(string title)
        {
            var ormEntity = context.Certificates.FirstOrDefault(entity => entity.title == title);
            return mapper.MapToDal(ormEntity);
        }

        public new void Delete(DalCertificate entity)
        {
            var ormEntity = context.Set<Certificate>().Single(Certificate => Certificate.id == entity.Id);
            context.Set<Certificate>().Remove(ormEntity);
        }

        public new DalCertificate Get(int id)
        {
            var ormEntity = context.Set<Certificate>().FirstOrDefault(Certificate => Certificate.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalCertificate> GetAll()
        {
            var elements = context.Set<Certificate>().Select(Certificate => Certificate);
            var retElemets = new List<DalCertificate>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalCertificate entity)
        {
            var ormEntity = context.Set<Certificate>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new Certificate Create(DalCertificate entity)
        {
            var res = context.Set<Certificate>().Add(mapper.MapToOrm(entity));
            return res;
        }


    }
}
