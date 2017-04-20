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
    public class SelectedControlNameService : Service<BllSelectedControlName, DalSelectedControlName>, ISelectedControlNameService
    {
        private readonly IUnitOfWork uow;

        public SelectedControlNameService(IUnitOfWork uow) : base(uow, uow.SelectedControlNames)
        {
            this.uow = uow;
            bllMapper = new SelectedControlNameMapper(uow);
        }
        ISelectedControlNameMapper bllMapper;
        public override void Create(BllSelectedControlName entity)
        {
            uow.SelectedControlNames.Create(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Update(BllSelectedControlName entity)
        {
            uow.SelectedControlNames.Update(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllSelectedControlName entity)
        {
            uow.SelectedControlNames.Delete(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override IEnumerable<BllSelectedControlName> GetAll()
        {
            var elements = uow.SelectedControlNames.GetAll();
            var retElemets = new List<BllSelectedControlName>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }

        public override BllSelectedControlName Get(int id)
        {
            return bllMapper.MapToBll(uow.SelectedControlNames.Get(id));
        }
    }
}
