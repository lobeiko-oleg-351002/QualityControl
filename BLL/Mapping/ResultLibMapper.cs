using BLL.Entities;
using BLL.Mapping.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class ResultLibMapper : IResultLibMapper
    {
        IUnitOfWork uow;
        public ResultLibMapper(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public DalResultLib MapToDal(BllResultLib entity)
        {
            return new DalResultLib
            {
                Id = entity.Id
            };
        }

        public BllResultLib MapToBll(DalResultLib entity)
        {
            BllResultLib bllEntity = new BllResultLib
            {
                Id = entity.Id
            };

            IResultMapper resultMapper = new ResultMapper();

            foreach (var Result in uow.Results.GetResultsByLibId(bllEntity.Id))
            {
                var bllSelectedResult = resultMapper.MapToBll(Result);
                bllEntity.Result.Add(bllSelectedResult);
            }
            return bllEntity;
        }
    }
}
