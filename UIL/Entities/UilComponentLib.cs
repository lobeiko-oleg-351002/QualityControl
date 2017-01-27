using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities.Interface;

namespace UIL.Entities
{
    public class UilComponentLib : IUilEntity
    {
        public int Id { get; set; }

        public List<UilSelectedComponent> SelectedComponent { get; set; }

        public UilComponentLib()
        {
            SelectedComponent = new List<UilSelectedComponent>();
        }
    }
}
