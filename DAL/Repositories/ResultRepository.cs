using AutoMapper;
using DAL.Entities;
using DAL.Mapping;
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
            var ormEntity = context.Results.FirstOrDefault(entity => entity.number == number);
            return mapper.MapToDal(ormEntity);
        }

        public new Result Create(DalResult entity)
        {
            var ormEntity = mapper.MapToOrm(entity);
            ormEntity.ResultLib = context.ResultLibs.FirstOrDefault(e => e.id == ormEntity.resultLib_id);
            return context.Set<Result>().Add(ormEntity);
        }
        public IEnumerable<DalResult> GetResultsByLibId(int id)
        {
            var elements = context.Set<Result>().Where(entity => entity.resultLib_id == id);
            var retElemets = new List<DalResult>();
            foreach (var element in elements)
            {
                retElemets.Add(mapper.MapToDal(element));
            }
            return retElemets;
        }

        ResultMapper mapper = new ResultMapper();

        public new void Delete(DalResult entity)
        {
            var ormEntity = context.Set<Result>().Single(Result => Result.id == entity.Id);
            context.Set<Result>().Remove(ormEntity);
        }

        public new DalResult Get(int id)
        {
            var ormEntity = context.Set<Result>().FirstOrDefault(Result => Result.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalResult> GetAll()
        {
            var elements = context.Set<Result>().Select(Result => Result);
            var retElemets = new List<DalResult>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalResult entity)
        {
            var ormEntity = context.Set<Result>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

    }
}
