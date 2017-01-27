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
    public class SelectedCertificateService : Service<BllSelectedCertificate, DalSelectedCertificate>, ISelectedCertificateService
    {
        private readonly IUnitOfWork uow;

        public SelectedCertificateService(IUnitOfWork uow) : base(uow, uow.SelectedCertificates)
        {
            this.uow = uow;
        }

    }
}
