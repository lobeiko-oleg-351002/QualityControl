using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
using BLL.Mapping.Interfaces;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ResultLibService : Service<BllResultLib, DalResultLib>, IResultLibService
    {
        private readonly IUnitOfWork uow;
        IResultLibMapper bllMapper;
        public ResultLibService(IUnitOfWork uow) : base(uow, uow.ResultLibs)
        {
            this.uow = uow;
            bllMapper = new ResultLibMapper(uow);
        }

        public new BllResultLib Create(BllResultLib entity)
        {
            var ormEntity = uow.ResultLibs.Create(bllMapper.MapToDal(entity));
            uow.Commit();
            entity.Id = ormEntity.id;
            IResultMapper resultMapper = new ResultMapper();
            foreach (var Result in entity.Result)
            {
                var dalResult = resultMapper.MapToDal(Result);
                dalResult.ResultLib_id = entity.Id;
                var ormResult = uow.Results.Create(dalResult);
                uow.Commit();
                Result.Id = ormResult.id;
            }

            return entity;
        }

        public override BllResultLib Get(int id)
        {
            var retElement = bllMapper.MapToBll(uow.ResultLibs.Get(id));
            var Results = uow.Results.GetResultsByLibId(retElement.Id);
            IResultMapper resultMapper = new ResultMapper();
            foreach (var Result in Results)
            {
                retElement.Result.Add(resultMapper.MapToBll(Result));
            }

            return retElement;
        }

        public new BllResultLib Update(BllResultLib entity)
        {
            IResultMapper resultMapper = new ResultMapper();
            foreach (var Result in entity.Result)
            {
                var dalResult = resultMapper.MapToDal(Result);
                dalResult.ResultLib_id = entity.Id;
                if (Result.Id == 0)
                {                                        
                    Result ormEntity = uow.Results.Create(dalResult);
                    uow.Commit();
                    Result.Id = ormEntity.id;
                }
                else
                {
                    uow.Results.Update(dalResult);
                    uow.Commit();
                }
               
            }

            var ResultsWithLibId = uow.Results.GetResultsByLibId(entity.Id);
            foreach (var Result in ResultsWithLibId)
            {
                bool isTrashResult = true;
                foreach (var result in entity.Result)
                {
                    if (Result.Id == result.Id)
                    {
                        isTrashResult = false;
                        break;
                    }
                }
                if (isTrashResult == true)
                {
                    uow.Results.Delete(Result);
                }
            }

            uow.Commit();

            return entity;
        }
    }
}
