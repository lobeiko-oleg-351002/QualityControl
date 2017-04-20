using BLL.Entities;
using BLL.Mapping.Interfaces;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class ResultMapper : IResultMapper
    {
        public ResultMapper()
        {

        }

        public DalResult MapToDal(BllResult entity)
        {
            DalResult dalEntity = new DalResult
            {
                Id = entity.Id,
                DefectDescription = entity.DefectDescription,
                Mark = entity.Mark,
                Norm = entity.Norm,
                Number = entity.Number,
                Quality = entity.Quality,
                Welder = entity.Welder,
            };

            return dalEntity;
        }

        public BllResult MapToBll(DalResult entity)
        {
            BllResult bllEntity = new BllResult
            {
                Id = entity.Id,
                DefectDescription = entity.DefectDescription,
                Mark = entity.Mark,
                Norm = entity.Norm,
                Number = entity.Number,
                Quality = entity.Quality,
                Welder = entity.Welder,
            };

            return bllEntity;
        }
    }
}
