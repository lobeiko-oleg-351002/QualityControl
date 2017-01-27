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
    public class ControlNameRepository : Repository<DalControlName, ControlName>, IControlNameRepository
    {
        private readonly ServiceDB context;
        public ControlNameRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalControlName GetControlNameByName(string name)
        {
            Mapper.CreateMap<ControlName, DalControlName>();
            var ormEntity = context.ControlNames.FirstOrDefault(entity => entity.name == name);
            return Mapper.Map<DalControlName>(ormEntity);
        }
    }
}
