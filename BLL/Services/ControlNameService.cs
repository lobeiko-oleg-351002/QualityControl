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
    public class ControlNameService : Service<BllControlName, DalControlName>, IControlNameService
    {
        private readonly IUnitOfWork uow;
        IControlNameMapper bllMapper;
        public ControlNameService(IUnitOfWork uow) : base(uow, uow.ControlNames)
        {
            this.uow = uow;
            bllMapper = new ControlNameMapper(uow);
        }

        public override void Create(BllControlName entity)
        {
            uow.ControlNames.Create(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Update(BllControlName entity)
        {
            uow.ControlNames.Update(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllControlName entity)
        {
            uow.ControlNames.Delete(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override IEnumerable<BllControlName> GetAll()
        {
            var elements = uow.ControlNames.GetAll();
            var retElemets = new List<BllControlName>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }

        public override BllControlName Get(int id)
        {
            return bllMapper.MapToBll(uow.ControlNames.Get(id));
        }

    }
}
