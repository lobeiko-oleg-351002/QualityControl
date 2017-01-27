using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllSelectedCertificate : IBllEntity
    {
        public int Id { get; set; }

        public BllCertificate Certificate { get; set; }
    }
}
