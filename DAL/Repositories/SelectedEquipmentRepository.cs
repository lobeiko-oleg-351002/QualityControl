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
    public class SelectedEquipmentRepository : Repository<DalSelectedEquipment, SelectedEquipment>, ISelectedEquipmentRepository
    {
        private readonly ServiceDB context;
        public SelectedEquipmentRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public new SelectedEquipment Create(DalSelectedEquipment entity)
        {
            Mapper.CreateMap<DalSelectedEquipment, SelectedEquipment>();
            var ormEntity = Mapper.Map<SelectedEquipment>(entity);
            ormEntity.EquipmentLib = context.EquipmentLibs.FirstOrDefault(e => e.id == ormEntity.equipmentLib_id);
            //ormEntity.SelectedEquipmentLib.SelectedEquipment.Add(ormEntity);
            return context.Set<SelectedEquipment>().Add(Mapper.Map<SelectedEquipment>(entity));
        }

        public IEnumerable<DalSelectedEquipment> GetEquipmentsByLibId(int id)
        {
            Mapper.CreateMap<SelectedEquipment, DalSelectedEquipment>();
            var elements = context.Set<SelectedEquipment>().Where(entity => entity.equipmentLib_id == id);
            var retElemets = new List<DalSelectedEquipment>();
            if (elements != null)
            {
                foreach (var element in elements)
                {
                    Mapper.CreateMap<SelectedEquipment, DalSelectedEquipment>();
                    retElemets.Add(Mapper.Map<DalSelectedEquipment>(element));
                }
            }

            return retElemets;
        }
    }
}
