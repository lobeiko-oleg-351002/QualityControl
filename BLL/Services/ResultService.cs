using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
using BLL.Mapping.Interfaces;
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
        IResultMapper bllMapper = new ResultMapper();
        public ResultService(IUnitOfWork uow) : base(uow, uow.Results)
        {
            this.uow = uow;
        }

        public override void Create(BllResult entity)
        {
            uow.Results.Create(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Update(BllResult entity)
        {
            uow.Results.Update(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllResult entity)
        {
            uow.Results.Delete(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override IEnumerable<BllResult> GetAll()
        {
            var elements = uow.Results.GetAll();
            var retElemets = new List<BllResult>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }

        public override BllResult Get(int id)
        {
            return bllMapper.MapToBll(uow.Results.Get(id));
        }

        public BllResult GetResultByNumber(int number)
        {
            return bllMapper.MapToBll(uow.Results.GetResultByNumber(number));
        }
    }
}
