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
    public class ResultRepository : Repository<UilResult, BllResult>, IResultRepository
    {
        private readonly IResultService ResultService;

        public ResultRepository() : base(UiUnitOfWork.Instance.Results)
        {
            ResultService = UiUnitOfWork.Instance.Results;
        }
    }
}
