﻿using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DalIndustrialObject : IDalEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Creator_id { get; set; }

        public int? Component_lib_id { get; set; }
    }
}
