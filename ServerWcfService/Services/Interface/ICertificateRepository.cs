using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities.Interface;

namespace ServerWcfService.Services.Interface
{
    [ServiceContract]
    public interface ICertificateRepository : IRepository<UilCertificate>
    {
        [OperationContract]
        UilCertificate GetCertificateByTitle(string title);
    }
}
