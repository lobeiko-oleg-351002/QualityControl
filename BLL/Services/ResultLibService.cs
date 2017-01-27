using AutoMapper;
using BLL.Entities;
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

        public ResultLibService(IUnitOfWork uow) : base(uow, uow.ResultLibs)
        {
            this.uow = uow;
        }

        public new BllResultLib Create(BllResultLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllResult, DalResult>();
                cfg.CreateMap<ResultLib, DalResultLib>();
                cfg.CreateMap<BllResultLib, DalResultLib>();
                cfg.CreateMap<DalResultLib, BllResultLib>();
            });
            var ormEntity = uow.ResultLibs.Create(Mapper.Map<DalResultLib>(entity));
            uow.Commit();
            entity.Id = ormEntity.id;
            foreach (var Result in entity.Result)
            {
                Mapper.CreateMap<BllResult, DalResult>();
                var dalResult = Mapper.Map<DalResult>(Result);
                dalResult.ResultLib_id = entity.Id;
                var ormResult = uow.Results.Create(dalResult);
                uow.Commit();
                Result.Id = ormResult.id;
            }

            return entity;
        }

        public override BllResultLib Get(int id)
        {
            Mapper.CreateMap<DalResultLib, BllResultLib>();
            var retElement = Mapper.Map<BllResultLib>(uow.ResultLibs.Get(id));
            var Results = uow.Results.GetResultsByLibId(retElement.Id);
            foreach (var Result in Results)
            {
                Mapper.CreateMap<DalResult, BllResult>();
                retElement.Result.Add(Mapper.Map<BllResult>(Result));
            }

            return retElement;
        }

        public override void Update(BllResultLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllResult, DalResult>();
                cfg.CreateMap<ResultLib, DalResultLib>();
                cfg.CreateMap<BllResultLib, DalResultLib>();
                cfg.CreateMap<DalResultLib, BllResultLib>();
            });
            foreach (var Result in entity.Result)
            {
                Mapper.CreateMap<BllResult, DalResult>();
                var dalResult = Mapper.Map<DalResult>(Result);
                dalResult.ResultLib_id = entity.Id;
                if (Result.Id == 0)
                {                                        
                    Results ormEntity = uow.Results.Create(dalResult);
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
        }
    }
}
