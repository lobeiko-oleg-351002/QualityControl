using BLL.Entities;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping.Interfaces
{
    public interface IJournalMapper
    {
        DalJournal MapToDal(BllJournal entity);
        BllJournal MapToBll(DalJournal entity);
    }
}
