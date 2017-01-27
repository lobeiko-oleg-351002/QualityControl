﻿using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class DalImage : IDalEntity
    {
        public int Id { get; set; }

        public byte[] Image { get; set; }

        public int? Image_lib_id { get; set; }
    }
}
