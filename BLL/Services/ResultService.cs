using AutoMapper;
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
    public class ResultService : Service<BllResult, DalResult>, IResultService
    {
        private readonly IUnitOfWork uow;

        public ResultService(IUnitOfWork uow) : base(uow, uow.Results)
        {
            this.uow = uow;
        }

        public BllResult GetResultByNumber(int number)
        {
            Mapper.CreateMap<DalResult, BllResult>();
            return Mapper.Map<BllResult>(uow.Results.GetResultByNumber(number));
        }
    }
}
