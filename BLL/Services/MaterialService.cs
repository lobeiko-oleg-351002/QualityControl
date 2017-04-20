using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
using BLL.Mapping.Interfaces;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class MaterialService : Service<BllMaterial, DalMaterial>, IMaterialService
    {
        private readonly IUnitOfWork uow;
        IMaterialMapper bllMapper = new MaterialMapper();
        public MaterialService(IUnitOfWork uow) : base(uow, uow.Materials)
        {
            this.uow = uow;
        }

        public override void Create(BllMaterial entity)
        {
            uow.Materials.Create(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Update(BllMaterial entity)
        {
            uow.Materials.Update(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllMaterial entity)
        {
            uow.Materials.Delete(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override IEnumerable<BllMaterial> GetAll()
        {
            var elements = uow.Materials.GetAll();
            var retElemets = new List<BllMaterial>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }

        public override BllMaterial Get(int id)
        {
            return bllMapper.MapToBll(uow.Materials.Get(id));
        }

        public BllMaterial GetMaterialByName(string name)
        {
            return bllMapper.MapToBll(uow.Materials.GetMaterialByName(name));
        }
    }
}
