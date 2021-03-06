﻿using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface IJournalRepository : IRepository<DalJournal>
    {
        new Journal Create(DalJournal entity);
        new Journal Update(DalJournal entity);
    }
}
