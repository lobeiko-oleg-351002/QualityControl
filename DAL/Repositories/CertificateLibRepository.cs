
using DAL.Entities;
using DAL.Mapping;
using DAL.Repositories.Interface;
using ORM;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class CertificateLibRepository : Repository<DalCertificateLib, CertificateLib>, ICertificateLibRepository
    {
        private readonly ServiceDB context;
        
        public CertificateLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        CertificateLibMapper mapper = new CertificateLibMapper();

        public new void Delete(DalCertificateLib entity)
        {
            var ormEntity = context.Set<CertificateLib>().Single(CertificateLib => CertificateLib.id == entity.Id);
            context.Set<CertificateLib>().Remove(ormEntity);
        }

        public new DalCertificateLib Get(int id)
        {
            var ormEntity = context.Set<CertificateLib>().FirstOrDefault(CertificateLib => CertificateLib.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new  IEnumerable<DalCertificateLib> GetAll()
        {
            var elements = context.Set<CertificateLib>().Select(CertificateLib => CertificateLib);
            var retElemets = new List<DalCertificateLib>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalCertificateLib entity)
        {
            var ormEntity = context.Set<CertificateLib>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new CertificateLib Create(DalCertificateLib entity)
        {      
            var res = context.Set<CertificateLib>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
