using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities.Interface;

namespace UIL.Entities
{
    public class UilResultLib : IUilEntity
    {
        public int Id { get; set; }

        public List<UilResult> Result { get; set; }

        public UilResultLib()
        {
            Result = new List<UilResult>();
        }
    }
}
