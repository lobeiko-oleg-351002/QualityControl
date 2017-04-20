using BLL.Entities;
using BLL.Mapping;
using BLL.Mapping.Interfaces;
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
    public class SelectedCertificateService : Service<BllSelectedCertificate, DalSelectedCertificate>, ISelectedCertificateService
    {
        private readonly IUnitOfWork uow;

        public SelectedCertificateService(IUnitOfWork uow) : base(uow, uow.SelectedCertificates)
        {
            this.uow = uow;
            bllMapper = new SelectedCertificateMapper(uow);
        }
        ISelectedCertificateMapper bllMapper;
        public override void Create(BllSelectedCertificate entity)
        {
            uow.SelectedCertificates.Create(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Update(BllSelectedCertificate entity)
        {
            uow.SelectedCertificates.Update(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllSelectedCertificate entity)
        {
            uow.SelectedCertificates.Delete(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override IEnumerable<BllSelectedCertificate> GetAll()
        {
            var elements = uow.SelectedCertificates.GetAll();
            var retElemets = new List<BllSelectedCertificate>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }

        public override BllSelectedCertificate Get(int id)
        {
            return bllMapper.MapToBll(uow.SelectedCertificates.Get(id));
        }



    }
}
