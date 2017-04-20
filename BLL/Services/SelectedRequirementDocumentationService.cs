using BLL.Entities;
using BLL.Mapping;
using BLL.Mapping.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SelectedRequirementDocumentationService : Service<BllSelectedRequirementDocumentation, DalSelectedRequirementDocumentation>, ISelectedRequirementDocumentationService
    {
        private readonly IUnitOfWork uow;

        public SelectedRequirementDocumentationService(IUnitOfWork uow) : base(uow, uow.SelectedRequirementDocumentations)
        {
            this.uow = uow;
            bllMapper = new SelectedRequirementDocumentationMapper(uow);
        }
        ISelectedRequirementDocumentationMapper bllMapper;
        public override void Create(BllSelectedRequirementDocumentation entity)
        {
            uow.SelectedRequirementDocumentations.Create(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Update(BllSelectedRequirementDocumentation entity)
        {
            uow.SelectedRequirementDocumentations.Update(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllSelectedRequirementDocumentation entity)
        {
            uow.SelectedRequirementDocumentations.Delete(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override IEnumerable<BllSelectedRequirementDocumentation> GetAll()
        {
            var elements = uow.SelectedRequirementDocumentations.GetAll();
            var retElemets = new List<BllSelectedRequirementDocumentation>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }

        public override BllSelectedRequirementDocumentation Get(int id)
        {
            return bllMapper.MapToBll(uow.SelectedRequirementDocumentations.Get(id));
        }
    }
}
