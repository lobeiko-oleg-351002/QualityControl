using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping.Interfaces
{
    public interface IJournalMapper
    {
        DalJournal MapToDal(Journal entity);
        Journal MapToOrm(DalJournal entity);
    }
}
