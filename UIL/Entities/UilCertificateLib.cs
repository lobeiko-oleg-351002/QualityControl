using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities.Interface;

namespace UIL.Entities
{
    public class UilCertificateLib : IUilEntity
    {
        public int Id { get; set; }

        public List<UilSelectedCertificate> SelectedCertificate { get; set; }

        public UilCertificateLib()
        {
            SelectedCertificate = new List<UilSelectedCertificate>();
        }
    }
}
