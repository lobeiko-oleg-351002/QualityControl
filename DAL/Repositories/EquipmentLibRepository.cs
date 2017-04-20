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
    public class EquipmentLibRepository : Repository<DalEquipmentLib, EquipmentLib>, IEquipmentLibRepository
    {
        private readonly ServiceDB context;
        public EquipmentLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        EquipmentLibMapper mapper = new EquipmentLibMapper();

        public new void Delete(DalEquipmentLib entity)
        {
            var ormEntity = context.Set<EquipmentLib>().Single(EquipmentLib => EquipmentLib.id == entity.Id);
            context.Set<EquipmentLib>().Remove(ormEntity);
        }

        public new DalEquipmentLib Get(int id)
        {
            var ormEntity = context.Set<EquipmentLib>().FirstOrDefault(EquipmentLib => EquipmentLib.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalEquipmentLib> GetAll()
        {
            var elements = context.Set<EquipmentLib>().Select(EquipmentLib => EquipmentLib);
            var retElemets = new List<DalEquipmentLib>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalEquipmentLib entity)
        {
            var ormEntity = context.Set<EquipmentLib>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new EquipmentLib Create(DalEquipmentLib entity)
        {
            var res = context.Set<EquipmentLib>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
