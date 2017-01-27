using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities.Interface;

namespace UIL.Entities
{
    public class UilEmployeeLib : IUilEntity
    {
        public int Id { get; set; }

        public List<UilSelectedEmployee> SelectedEmployee { get; set; }

        public UilEmployeeLib()
        {
            SelectedEmployee = new List<UilSelectedEmployee>();
        }
    }
}
