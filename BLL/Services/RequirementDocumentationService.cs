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
    public class RequirementDocumentationService : Service<BllRequirementDocumentation, DalRequirementDocumentation>, IRequirementDocumentationService
    {
        private readonly IUnitOfWork uow;
        IRequirementDocumentationMapper bllMapper = new RequirementDocumentationMapper();
        public RequirementDocumentationService(IUnitOfWork uow) : base(uow, uow.RequirementDocumentations)
        {
            this.uow = uow;
        }

        public override void Create(BllRequirementDocumentation entity)
        {
            uow.RequirementDocumentations.Create(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Update(BllRequirementDocumentation entity)
        {
            uow.RequirementDocumentations.Update(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllRequirementDocumentation entity)
        {
            uow.RequirementDocumentations.Delete(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override IEnumerable<BllRequirementDocumentation> GetAll()
        {
            var elements = uow.RequirementDocumentations.GetAll();
            var retElemets = new List<BllRequirementDocumentation>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }

        public override BllRequirementDocumentation Get(int id)
        {
            return bllMapper.MapToBll(uow.RequirementDocumentations.Get(id));
        }

        public BllRequirementDocumentation GetRequirementDocumentationByName(string name)
        {
            return bllMapper.MapToBll(uow.RequirementDocumentations.GetRequirementDocumentationByName(name));
        }
    }
}
