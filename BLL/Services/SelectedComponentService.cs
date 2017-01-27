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
    public class SelectedComponentService : Service<BllSelectedComponent, DalSelectedComponent>, ISelectedComponentService
    {
        private readonly IUnitOfWork uow;

        public SelectedComponentService(IUnitOfWork uow) : base(uow, uow.SelectedComponents)
        {
            this.uow = uow;
        }
    }
}
