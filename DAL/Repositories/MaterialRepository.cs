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
    public class MaterialRepository : Repository<DalMaterial, Material>, IMaterialRepository
    {
        private readonly ServiceDB context;
        public MaterialRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalMaterial GetMaterialByName(string name)
        {
            Mapper.CreateMap<Material, DalMaterial>();
            var ormEntity = context.Materials.FirstOrDefault(entity => entity.name == name);
            return Mapper.Map<DalMaterial>(ormEntity);
        }
    }
}
