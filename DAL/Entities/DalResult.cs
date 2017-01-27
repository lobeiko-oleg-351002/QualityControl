using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DalResult : IDalEntity
    {
        public int Id { get; set; }

        public int? Number { get; set; }

        public string Welder { get; set; }

        public string Mark { get; set; }

        public string Defect_description { get; set; }

        public string Norm { get; set; }

        public string Quality { get; set; }

        public int ResultLib_id { get; set; }
    }
}
