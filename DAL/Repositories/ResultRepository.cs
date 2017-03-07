using AutoMapper;
using DAL.Entities;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ResultRepository : Repository<DalResult, Result>, IResultRepository
    {
        private readonly ServiceDB context;
        public ResultRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalResult GetResultByNumber(int number)
        {
            Mapper.CreateMap<Result, DalResult>();
            var ormEntity = context.Results.FirstOrDefault(entity => entity.number == number);
            return Mapper.Map<DalResult>(ormEntity);
        }

        public new Result Create(DalResult entity)
        {
            Mapper.CreateMap<DalResult, Result>();
            var ormEntity = Mapper.Map<Result>(entity);
            ormEntity.ResultLib = context.ResultLibs.FirstOrDefault(e => e.id == ormEntity.resultLib_id);
            //ormEntity.ResultLib.Result.Add(ormEntity);
            return context.Set<Result>().Add(Mapper.Map<Result>(entity));
        }
        public IEnumerable<DalResult> GetResultsByLibId(int id)
        {
            Mapper.CreateMap<Result, DalResult>();
            var elements = context.Set<Result>().Where(entity => entity.resultLib_id == id);
            var retElemets = new List<DalResult>();
            foreach (var element in elements)
            {
                Mapper.CreateMap<Result, DalResult>();
                retElemets.Add(Mapper.Map<DalResult>(element));
            }
            return retElemets;
        }
    }
}
