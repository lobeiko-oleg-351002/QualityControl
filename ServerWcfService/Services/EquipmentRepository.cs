using AutoMapper;
using BLL.Entities;
using BLL.Services.Interface;
using ServerWcfService.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities;

namespace ServerWcfService.Services
{
    public class EquipmentRepository : Repository<UilEquipment, BllEquipment>, IEquipmentRepository
    {
        private readonly IEquipmentService equipmentService;

        public EquipmentRepository() : base(UiUnitOfWork.Instance.Equipments)
        {
            equipmentService = UiUnitOfWork.Instance.Equipments;
        }

        public IEnumerable<UilEquipment> GetCheckedEquipment()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllEquipment, UilEquipment>();
            });
            var elements = equipmentService.GetCheckedEquipment();
            var retElemets = new List<UilEquipment>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<UilEquipment>(element));
            }
            return retElemets;
        }

        public IEnumerable<UilEquipment> GetEquipmentByFactoryNumber(int number)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllEquipment, UilEquipment>();
            });
            var elements = equipmentService.GetEquipmentByFactoryNumber(number);
            var retElemets = new List<UilEquipment>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<UilEquipment>(element));
            }
            return retElemets;
        }

        public UilEquipment GetEquipmentByName(string name)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllEquipment, UilEquipment>();
            });
            return Mapper.Map<UilEquipment>(equipmentService.GetEquipmentByName(name));
        }

        public IEnumerable<UilEquipment> GetEquipmentByType(string type)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllEquipment, UilEquipment>();
            });
            var elements = equipmentService.GetEquipmentByType(type);
            var retElemets = new List<UilEquipment>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<UilEquipment>(element));
            }
            return retElemets;
        }

        public IEnumerable<UilEquipment> GetUncheckedEquipment()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllEquipment, UilEquipment>();
            });
            var elements = equipmentService.GetUncheckedEquipment();
            var retElemets = new List<UilEquipment>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<UilEquipment>(element));
            }
            return retElemets;
        }
    }
}
