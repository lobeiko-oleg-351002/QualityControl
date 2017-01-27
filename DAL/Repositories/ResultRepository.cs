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
    public class ResultRepository : Repository<DalResult, Results>, IResultRepository
    {
        private readonly ServiceDB context;
        public ResultRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalResult GetResultByNumber(int number)
        {
            Mapper.CreateMap<Results, DalResult>();
            var ormEntity = context.Results.FirstOrDefault(entity => entity.number == number);
            return Mapper.Map<DalResult>(ormEntity);
        }

        public new Results Create(DalResult entity)
        {
            Mapper.CreateMap<DalResult, Results>();
            var ormEntity = Mapper.Map<Results>(entity);
            ormEntity.ResultLib = context.ResultLibs.FirstOrDefault(e => e.id == ormEntity.resultLib_id);
            //ormEntity.ResultLib.Result.Add(ormEntity);
            return context.Set<Results>().Add(Mapper.Map<Results>(entity));
        }
        public IEnumerable<DalResult> GetResultsByLibId(int id)
        {
            Mapper.CreateMap<Results, DalResult>();
            var elements = context.Set<Results>().Where(entity => entity.resultLib_id == id);
            var retElemets = new List<DalResult>();
            foreach (var element in elements)
            {
                Mapper.CreateMap<Results, DalResult>();
                retElemets.Add(Mapper.Map<DalResult>(element));
            }
            return retElemets;
        }
    }
}
