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
    public class SelectedEquipmentService : Service<BllSelectedEquipment, DalSelectedEquipment>, ISelectedEquipmentService
    {
        private readonly IUnitOfWork uow;

        public SelectedEquipmentService(IUnitOfWork uow) : base(uow, uow.SelectedEquipments)
        {
            this.uow = uow;
            bllMapper = new SelectedEquipmentMapper(uow);
        }
        ISelectedEquipmentMapper bllMapper;
        public override void Create(BllSelectedEquipment entity)
        {
            uow.SelectedEquipments.Create(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Update(BllSelectedEquipment entity)
        {
            uow.SelectedEquipments.Update(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllSelectedEquipment entity)
        {
            uow.SelectedEquipments.Delete(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override IEnumerable<BllSelectedEquipment> GetAll()
        {
            var elements = uow.SelectedEquipments.GetAll();
            var retElemets = new List<BllSelectedEquipment>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }

        public override BllSelectedEquipment Get(int id)
        {
            return bllMapper.MapToBll(uow.SelectedEquipments.Get(id));
        }

    }
}
