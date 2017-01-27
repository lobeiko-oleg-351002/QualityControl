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
    public class SelectedCertificateRepository : Repository<DalSelectedCertificate, SelectedCertificate>, ISelectedCertificateRepository
    {
        private readonly ServiceDB context;
        public SelectedCertificateRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<DalSelectedCertificate> GetCertificatesByLibId(int id)
        {
            Mapper.CreateMap<SelectedCertificate, DalSelectedCertificate>();
            var elements = context.Set<SelectedCertificate>().Where(entity => entity.certificate_lib_id == id);
            var retElemets = new List<DalSelectedCertificate>();
            foreach (var element in elements)
            {
                Mapper.CreateMap<SelectedCertificate, DalSelectedCertificate>();
                retElemets.Add(Mapper.Map<DalSelectedCertificate>(element));
            }
            return retElemets;
        }
    }
}
