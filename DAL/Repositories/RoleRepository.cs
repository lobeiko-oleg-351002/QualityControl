using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;
using AutoMapper;
using DAL.Mapping;

namespace DAL.Repositories
{
    public class RoleRepository : Repository<DalRole, Role>, IRoleRepository
    {
        private readonly ServiceDB context;
        public RoleRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalRole GetRoleByName(string name)
        {
           
            var ormEntity = context.Roles.FirstOrDefault(entity => entity.name == name);
            return mapper.MapToDal(ormEntity);
        }

        RoleMapper mapper = new RoleMapper();

        public new void Delete(DalRole entity)
        {
            var ormEntity = context.Set<Role>().Single(Role => Role.id == entity.Id);
            context.Set<Role>().Remove(ormEntity);
        }

        public new DalRole Get(int id)
        {
            var ormEntity = context.Set<Role>().FirstOrDefault(Role => Role.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalRole> GetAll()
        {
            var elements = context.Set<Role>().Select(Role => Role);
            var retElemets = new List<DalRole>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalRole entity)
        {
            var ormEntity = context.Set<Role>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new Role Create(DalRole entity)
        {
            var res = context.Set<Role>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
