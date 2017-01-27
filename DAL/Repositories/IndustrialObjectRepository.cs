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
    public class IndustrialObjectRepository: Repository<DalIndustrialObject, IndustrialObject>, IIndustrialObjectRepository
    {
        private readonly ServiceDB context;
        public IndustrialObjectRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public DalIndustrialObject GetIndustrialObjectByName(string name)
        {
            Mapper.CreateMap<IndustrialObject, DalIndustrialObject>();
            var ormEntity = context.IndustrialObjects.FirstOrDefault(entity => entity.name == name);
            return Mapper.Map<DalIndustrialObject>(ormEntity);
        }
    }
}
