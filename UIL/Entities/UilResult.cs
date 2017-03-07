using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities.Interface;

namespace UIL.Entities
{
    public class UilResult : IUilEntity
    {
        public int Id { get; set; }

        public int? Number { get; set; }

        public string Welder { get; set; }

        public string Mark { get; set; }

        public string DefectDescription { get; set; }

        public string Norm { get; set; }

        public string Quality { get; set; }
    }
}
