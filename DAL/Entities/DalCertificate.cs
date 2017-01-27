using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DalCertificate : IDalEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CheckDate { get; set; }

        public int? Creator_id { get; set; }

        public int? ControlName_id { get; set; }

        public int? Duration { get; set; }

        public int? Employee_id { get; set; }
    }
}
