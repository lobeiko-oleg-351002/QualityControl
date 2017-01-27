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
    public class EquipmentLibRepository : Repository<DalEquipmentLib, EquipmentLib>, IEquipmentLibRepository
    {
        private readonly ServiceDB context;
        public EquipmentLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public new EquipmentLib Create(DalEquipmentLib entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DalEquipmentLib, EquipmentLib>();
                cfg.CreateMap<EquipmentLib, DalEquipmentLib>();
            });
            var res = context.Set<EquipmentLib>().Add(Mapper.Map<EquipmentLib>(entity));
            return res;
        }
    }
}
