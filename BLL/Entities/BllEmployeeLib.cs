using BLL.Entities.Interface;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllEmployeeLib : IBllEntity
    {
        public int Id { get; set; }

        public List<BllSelectedEmployee> SelectedEmployee { get; set; }

        public BllEmployeeLib()
        {
            SelectedEmployee = new List<BllSelectedEmployee>();
        }
    }
}
