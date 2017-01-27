using BLL.Entities.Interface;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllControlNameLib : IBllEntity
    {
        public int Id { get; set; }

        public List<BllSelectedControlName> SelectedControlName { get; set; }

        public BllControlNameLib()
        {
            SelectedControlName = new List<BllSelectedControlName>();
        }
    }
}
