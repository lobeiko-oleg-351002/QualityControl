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
    public class SelectedComponentService : Service<BllSelectedComponent, DalSelectedComponent>, ISelectedComponentService
    {
        private readonly IUnitOfWork uow;

        public SelectedComponentService(IUnitOfWork uow) : base(uow, uow.SelectedComponents)
        {
            this.uow = uow;
            bllMapper = new SelectedComponentMapper(uow);
        }
        ISelectedComponentMapper bllMapper;
        public override void Create(BllSelectedComponent entity)
        {
            uow.SelectedComponents.Create(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Update(BllSelectedComponent entity)
        {
            uow.SelectedComponents.Update(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllSelectedComponent entity)
        {
            uow.SelectedComponents.Delete(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override IEnumerable<BllSelectedComponent> GetAll()
        {
            var elements = uow.SelectedComponents.GetAll();
            var retElemets = new List<BllSelectedComponent>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }

        public override BllSelectedComponent Get(int id)
        {
            return bllMapper.MapToBll(uow.SelectedComponents.Get(id));
        }
    }
}
