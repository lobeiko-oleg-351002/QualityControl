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
    public class MaterialRepository : Repository<DalMaterial, Material>, IMaterialRepository
    {
        private readonly ServiceDB context;
        public MaterialRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalMaterial GetMaterialByName(string name)
        {
            var ormEntity = context.Materials.FirstOrDefault(entity => entity.name == name);
            return mapper.MapToDal(ormEntity);
        }

        MaterialMapper mapper = new MaterialMapper();

        public new void Delete(DalMaterial entity)
        {
            var ormEntity = context.Set<Material>().Single(Material => Material.id == entity.Id);
            context.Set<Material>().Remove(ormEntity);
        }

        public new DalMaterial Get(int id)
        {
            var ormEntity = context.Set<Material>().FirstOrDefault(Material => Material.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalMaterial> GetAll()
        {
            var elements = context.Set<Material>().Select(Material => Material);
            var retElemets = new List<DalMaterial>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalMaterial entity)
        {
            var ormEntity = context.Set<Material>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new Material Create(DalMaterial entity)
        {
            var res = context.Set<Material>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
