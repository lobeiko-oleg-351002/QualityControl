using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllCertificate : IBllEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CheckDate { get; set; }

        public int? Creator_id { get; set; }

        public BllControlName ControlName { get; set; }

        public int? Duration { get; set; }

        public BllEmployee Employee { get; set; }
    }
}
