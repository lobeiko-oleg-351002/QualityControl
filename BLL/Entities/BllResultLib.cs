using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllResultLib : IBllEntity
    {
        public int Id { get; set; }

        public List<BllResult> Result { get; set; }

        public BllResultLib()
        {
            Result = new List<BllResult>();
        }
    }
}
