using BLL.Entities;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SelectedControlNameService : Service<BllSelectedControlName, DalSelectedControlName>, ISelectedControlNameService
    {
        private readonly IUnitOfWork uow;

        public SelectedControlNameService(IUnitOfWork uow) : base(uow, uow.SelectedControlNames)
        {
            this.uow = uow;
        }
    }
}
