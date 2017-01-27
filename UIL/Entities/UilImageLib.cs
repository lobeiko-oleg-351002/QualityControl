using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities.Interface;

namespace UIL.Entities
{
    public class UilImageLib : IUilEntity
    {
        public int Id { get; set; }

        public List<UilImage> Image { get; set; }

        public UilImageLib()
        {
            Image = new List<UilImage>();
        }
    }
}
