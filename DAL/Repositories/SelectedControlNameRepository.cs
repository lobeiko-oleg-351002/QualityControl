using AutoMapper;
using DAL.Entities;
using DAL.Repositories;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SelectedControlNameRepository : Repository<DalSelectedControlName, SelectedControlName>, ISelectedControlNameRepository
    {
        private readonly ServiceDB context;
        public SelectedControlNameRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<DalSelectedControlName> GetControlNamesByLibId(int id)
        {
            Mapper.CreateMap<SelectedControlName, DalSelectedControlName>();
            var elements = context.Set<SelectedControlName>().Where(entity => entity.controlNameLib_id == id);
            var retElemets = new List<DalSelectedControlName>();
            foreach (var element in elements)
            {
                Mapper.CreateMap<SelectedControlName, DalSelectedControlName>();
                retElemets.Add(Mapper.Map<DalSelectedControlName>(element));
            }
            return retElemets;
        }
    }
}
