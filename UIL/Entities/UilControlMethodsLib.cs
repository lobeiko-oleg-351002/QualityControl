using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities.Interface;

namespace UIL.Entities
{
    public class UilControlMethodsLib : IUilEntity
    {
        public int Id { get; set; }

        public List<UilControl> Control { get; set; }

        public UilControlMethodsLib()
        {
            Control = new List<UilControl>();
        }
    }
}
