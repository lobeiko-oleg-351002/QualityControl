using DAL.Entities;
using DAL.Mapping.Interfaces;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping
{
    public class ResultMapper : IResultMapper
    {
        public DalResult MapToDal(Result entity)
        {
            return new DalResult
            {
                Id = entity.id,
                DefectDescription = entity.defectDescription,
                Mark = entity.mark,
                Norm = entity.norm,
                Number = entity.number,
                Quality = entity.quality,
                ResultLib_id = entity.resultLib_id.Value,
                Welder = entity.welder
            };
        }

        public Result MapToOrm(DalResult entity)
        {
            return new Result
            {
                id = entity.Id,
                defectDescription = entity.DefectDescription,
                mark = entity.Mark,
                norm = entity.Norm,
                number = entity.Number,
                quality = entity.Quality,
                resultLib_id = entity.ResultLib_id,
                welder = entity.Welder
            };
        }
    }
}
