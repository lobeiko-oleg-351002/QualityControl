using AutoMapper;
using DAL.Entities;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EquipmentRepository: Repository<DalEquipment,Equipment>, IEquipmentRepository
    {
        private readonly ServiceDB context;
        public EquipmentRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<DalEquipment> GetCheckedEquipment()
        {
            Mapper.CreateMap<Equipment, DalEquipment>();
            var elements = context.Equipments.Select(entity => entity.isChecked[0] == 1);
            var retElemets = new List<DalEquipment>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<DalEquipment>(element));
            }
            return retElemets;
        }

        public IEnumerable<DalEquipment> GetEquipmentByFactoryNumber(int number)
        {
            Mapper.CreateMap<Equipment, DalEquipment>();
            var elements = context.Equipments.Select(entity => entity.factoryNumber == number);
            var retElemets = new List<DalEquipment>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<DalEquipment>(element));
            }
            return retElemets;
        }

        public DalEquipment GetEquipmentByName(string name)
        {
            Mapper.CreateMap<Equipment, DalEquipment>();
            var ormEntity = context.Equipments.FirstOrDefault(entity => entity.name == name);
            return Mapper.Map<DalEquipment>(ormEntity);
        }

        public IEnumerable<DalEquipment> GetEquipmentByType(string type)
        {
            Mapper.CreateMap<Equipment, DalEquipment>();
            var elements = context.Equipments.Select(entity => entity.type == type);
            var retElemets = new List<DalEquipment>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<DalEquipment>(element));
            }
            return retElemets;
        }

        public IEnumerable<DalEquipment> GetUncheckedEquipment()
        {
            Mapper.CreateMap<Equipment, DalEquipment>();
            var elements = context.Equipments.Select(entity => entity.isChecked[0] == 0);
            var retElemets = new List<DalEquipment>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<DalEquipment>(element));
            }
            return retElemets;
        }
    }
}
