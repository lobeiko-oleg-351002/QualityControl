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
    public class SelectedEquipmentRepository : Repository<DalSelectedEquipment, SelectedEquipment>, ISelectedEquipmentRepository
    {
        private readonly ServiceDB context;
        public SelectedEquipmentRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public new SelectedEquipment Create(DalSelectedEquipment entity)
        {
            var ormEntity = mapper.MapToOrm(entity);
            ormEntity.EquipmentLib = context.EquipmentLibs.FirstOrDefault(e => e.id == ormEntity.equipmentLib_id);
            //ormEntity.SelectedEquipmentLib.SelectedEquipment.Add(ormEntity);
            return context.Set<SelectedEquipment>().Add(ormEntity);
        }

        public IEnumerable<DalSelectedEquipment> GetEquipmentsByLibId(int id)
        {
            var elements = context.Set<SelectedEquipment>().Where(entity => entity.equipmentLib_id == id);
            var retElemets = new List<DalSelectedEquipment>();
            if (elements != null)
            {
                foreach (var element in elements)
                {                   
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        SelectedEquipmentMapper mapper = new SelectedEquipmentMapper();

        public new void Delete(DalSelectedEquipment entity)
        {
            var ormEntity = context.Set<SelectedEquipment>().Single(SelectedEquipment => SelectedEquipment.id == entity.Id);
            context.Set<SelectedEquipment>().Remove(ormEntity);
        }

        public new DalSelectedEquipment Get(int id)
        {
            var ormEntity = context.Set<SelectedEquipment>().FirstOrDefault(SelectedEquipment => SelectedEquipment.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalSelectedEquipment> GetAll()
        {
            var elements = context.Set<SelectedEquipment>().Select(SelectedEquipment => SelectedEquipment);
            var retElemets = new List<DalSelectedEquipment>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalSelectedEquipment entity)
        {
            var ormEntity = context.Set<SelectedEquipment>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

    }
}
