using AutoMapper;
using DAL.Entities;
using DAL.Mapping;
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
            var elements = context.Equipments.Select(entity => entity.isChecked);
            var retElemets = new List<DalEquipment>();
            foreach (var element in elements)
            {
                //retElemets.Add(Mapper.Map<DalEquipment>(element));
            }
            return retElemets;
        }

        public IEnumerable<DalEquipment> GetEquipmentByFactoryNumber(int number)
        {
            var elements = context.Equipments.Select(entity => entity.factoryNumber == number);
            var retElemets = new List<DalEquipment>();
            foreach (var element in elements)
            {
                //retElemets.Add((element));
            }
            return retElemets;
        }

        public DalEquipment GetEquipmentByName(string name)
        {
            var ormEntity = context.Equipments.FirstOrDefault(entity => entity.name == name);
            return mapper.MapToDal(ormEntity);
        }

        public IEnumerable<DalEquipment> GetEquipmentByType(string type)
        {
            var elements = context.Equipments.Select(entity => entity.type == type);
            var retElemets = new List<DalEquipment>();
            foreach (var element in elements)
            {
               // retElemets.Add(Mapper.Map<DalEquipment>(element));
            }
            return retElemets;
        }

        public IEnumerable<DalEquipment> GetUncheckedEquipment()
        {
            var elements = context.Equipments.Select(entity => entity.isChecked);
            var retElemets = new List<DalEquipment>();
            foreach (var element in elements)
            {
               // retElemets.Add(mapper.MapToDal(element));
            }
            return retElemets;
        }

        EquipmentMapper mapper = new EquipmentMapper();

        public new void Delete(DalEquipment entity)
        {
            var ormEntity = context.Set<Equipment>().Single(Equipment => Equipment.id == entity.Id);
            context.Set<Equipment>().Remove(ormEntity);
        }

        public new DalEquipment Get(int id)
        {
            var ormEntity = context.Set<Equipment>().FirstOrDefault(Equipment => Equipment.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalEquipment> GetAll()
        {
            var elements = context.Set<Equipment>().Select(Equipment => Equipment);
            var retElemets = new List<DalEquipment>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalEquipment entity)
        {
            var ormEntity = context.Set<Equipment>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new Equipment Create(DalEquipment entity)
        {
            var res = context.Set<Equipment>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
