using AutoMapper;
using BLL.Entities;
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

        public EquipmentService(IUnitOfWork uow) : base(uow, uow.Equipments)
        {
            this.uow = uow;
        }

        public IEnumerable<BllEquipment> GetCheckedEquipment()
        {
            Mapper.CreateMap<DalEquipment, BllEquipment>();
            var elements = uow.Equipments.GetCheckedEquipment();
            var retElemets = new List<BllEquipment>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<BllEquipment>(element));
            }
            return retElemets;
        }

        public IEnumerable<BllEquipment> GetEquipmentByFactoryNumber(int number)
        {
            Mapper.CreateMap<DalEquipment, BllEquipment>();
            var elements = uow.Equipments.GetEquipmentByFactoryNumber(number);
            var retElemets = new List<BllEquipment>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<BllEquipment>(element));
            }
            return retElemets;
        }

        public BllEquipment GetEquipmentByName(string name)
        {
            Mapper.CreateMap<DalEquipment, BllEquipment>();
            return Mapper.Map<BllEquipment>(uow.Equipments.GetEquipmentByName(name));
        }

        public IEnumerable<BllEquipment> GetEquipmentByType(string type)
        {
            Mapper.CreateMap<DalEquipment, BllEquipment>();
            var elements = uow.Equipments.GetEquipmentByType(type);
            var retElemets = new List<BllEquipment>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<BllEquipment>(element));
            }
            return retElemets;
        }

        public IEnumerable<BllEquipment> GetUncheckedEquipment()
        {
            Mapper.CreateMap<DalEquipment, BllEquipment>();
            var elements = uow.Equipments.GetUncheckedEquipment();
            var retElemets = new List<BllEquipment>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<BllEquipment>(element));
            }
            return retElemets;
        }
    }
}
