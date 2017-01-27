using BLL.Entities;
using BLL.Services.Interface;
using ServerWcfService.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities;

namespace ServerWcfService.Services
{
    public class ControlNameRepository : Repository<UilControlName, BllControlName>, IControlNameRepository
    {
        private readonly IControlNameService ControlNameService;

        public ControlNameRepository() : base(UiUnitOfWork.Instance.ControlNames)
        {
            ControlNameService = UiUnitOfWork.Instance.ControlNames;
        }
    }
}
