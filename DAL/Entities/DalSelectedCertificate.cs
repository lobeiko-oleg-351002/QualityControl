using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DalSelectedCertificate : IDalEntity
    {
        public int Id { get; set; }

        public int? Certificate_id { get; set; }

        public int? Certificate_lib_id { get; set; }
    }
}
