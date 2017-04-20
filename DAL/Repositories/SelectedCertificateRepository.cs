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
    public class SelectedCertificateRepository : Repository<DalSelectedCertificate, SelectedCertificate>, ISelectedCertificateRepository
    {
        private readonly ServiceDB context;
        public SelectedCertificateRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<DalSelectedCertificate> GetCertificatesByLibId(int id)
        {
            var elements = context.Set<SelectedCertificate>().Where(entity => entity.certificateLib_id == id);
            var retElemets = new List<DalSelectedCertificate>();
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapToDal(element));
            }
            return retElemets;
        }

        SelectedCertificateMapper mapper = new SelectedCertificateMapper();

        public new void Delete(DalSelectedCertificate entity)
        {
            var ormEntity = context.Set<SelectedCertificate>().Single(SelectedCertificate => SelectedCertificate.id == entity.Id);
            context.Set<SelectedCertificate>().Remove(ormEntity);
        }

        public new DalSelectedCertificate Get(int id)
        {
            var ormEntity = context.Set<SelectedCertificate>().FirstOrDefault(SelectedCertificate => SelectedCertificate.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalSelectedCertificate> GetAll()
        {
            var elements = context.Set<SelectedCertificate>().Select(SelectedCertificate => SelectedCertificate);
            var retElemets = new List<DalSelectedCertificate>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalSelectedCertificate entity)
        {
            var ormEntity = context.Set<SelectedCertificate>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new SelectedCertificate Create(DalSelectedCertificate entity)
        {
            var res = context.Set<SelectedCertificate>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
