using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities.Interface;

namespace UIL.Entities
{
    public class UilUser : IUilEntity
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public UilRole Role { get; set; }
        
        public UilEmployee Employee { get; set; }

        public DateTime? Modified_date { get; set; }
    }
}
