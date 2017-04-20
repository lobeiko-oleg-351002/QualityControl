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
    public class EmployeeLibRepository : Repository<DalEmployeeLib, EmployeeLib>, IEmployeeLibRepository
    {
        private readonly ServiceDB context;
        public EmployeeLibRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        EmployeeLibMapper mapper = new EmployeeLibMapper();

        public new void Delete(DalEmployeeLib entity)
        {
            var ormEntity = context.Set<EmployeeLib>().Single(EmployeeLib => EmployeeLib.id == entity.Id);
            context.Set<EmployeeLib>().Remove(ormEntity);
        }

        public new DalEmployeeLib Get(int id)
        {
            var ormEntity = context.Set<EmployeeLib>().FirstOrDefault(EmployeeLib => EmployeeLib.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalEmployeeLib> GetAll()
        {
            var elements = context.Set<EmployeeLib>().Select(EmployeeLib => EmployeeLib);
            var retElemets = new List<DalEmployeeLib>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalEmployeeLib entity)
        {
            var ormEntity = context.Set<EmployeeLib>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new EmployeeLib Create(DalEmployeeLib entity)
        {
            var res = context.Set<EmployeeLib>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
