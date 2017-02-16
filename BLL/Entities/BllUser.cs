using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllUser : IBllEntity
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public BllRole Role { get; set; }

        public BllEmployee Employee { get; set; }

        public DateTime? Modified_date { get; set; }


    }
}
