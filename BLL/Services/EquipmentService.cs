using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
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
    public class EquipmentService :Service<BllEquipment, DalEquipment>, IEquipmentService
    {
        private readonly IUnitOfWork uow;
        EquipmentMapper bllMapper = new EquipmentMapper();
        public EquipmentService(IUnitOfWork uow) : base(uow, uow.Equipments)
        {
            this.uow = uow;
        }

        public override void Create(BllEquipment entity)
        {
            uow.Equipments.Create(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Update(BllEquipment entity)
        {
            uow.Equipments.Update(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllEquipment entity)
        {
            uow.Equipments.Delete(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override IEnumerable<BllEquipment> GetAll()
        {
            var elements = uow.Equipments.GetAll();
            var retElemets = new List<BllEquipment>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }

        public override BllEquipment Get(int id)
        {
            return bllMapper.MapToBll(uow.Equipments.Get(id));
        }

        public IEnumerable<BllEquipment> GetCheckedEquipment()
        {
            var elements = uow.Equipments.GetCheckedEquipment();
            var retElemets = new List<BllEquipment>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }

        public IEnumerable<BllEquipment> GetEquipmentByFactoryNumber(int number)
        {
            var elements = uow.Equipments.GetEquipmentByFactoryNumber(number);
            var retElemets = new List<BllEquipment>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }

        public BllEquipment GetEquipmentByName(string name)
        {
            return bllMapper.MapToBll(uow.Equipments.GetEquipmentByName(name));
        }

        public IEnumerable<BllEquipment> GetEquipmentByType(string type)
        {
            var elements = uow.Equipments.GetEquipmentByType(type);
            var retElemets = new List<BllEquipment>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }

        public IEnumerable<BllEquipment> GetUncheckedEquipment()
        {
            var elements = uow.Equipments.GetUncheckedEquipment();
            var retElemets = new List<BllEquipment>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }
    }
}
