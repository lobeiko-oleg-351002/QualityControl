﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities.Interface;

namespace UIL.Entities
{
    public class UilRole : IUilEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
