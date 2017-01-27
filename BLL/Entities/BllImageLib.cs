using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllImageLib : IBllEntity
    {
        public int Id { get; set; }

        public List<BllImage> Image { get; set; }

        public BllImageLib()
        {
            Image = new List<BllImage>();
        }
    }
}
