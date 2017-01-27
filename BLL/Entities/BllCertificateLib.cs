using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllCertificateLib : IBllEntity
    {
        public int Id { get; set; }

        public List<BllSelectedCertificate> SelectedCertificate { get; set; }

        public BllCertificateLib()
        {
            SelectedCertificate = new List<BllSelectedCertificate>();
        }
    }
}
