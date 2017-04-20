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
    public class ResultLibRepository : Repository<DalResultLib, ResultLib>, IResultLibRepository
    {
        private readonly ServiceDB context;
        public ResultLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        ResultLibMapper mapper = new ResultLibMapper();

        public new void Delete(DalResultLib entity)
        {
            var ormEntity = context.Set<ResultLib>().Single(ResultLib => ResultLib.id == entity.Id);
            context.Set<ResultLib>().Remove(ormEntity);
        }

        public new DalResultLib Get(int id)
        {
            var ormEntity = context.Set<ResultLib>().FirstOrDefault(ResultLib => ResultLib.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalResultLib> GetAll()
        {
            var elements = context.Set<ResultLib>().Select(ResultLib => ResultLib);
            var retElemets = new List<DalResultLib>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalResultLib entity)
        {
            var ormEntity = context.Set<ResultLib>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new ResultLib Create(DalResultLib entity)
        {
            var res = context.Set<ResultLib>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}