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
    public class SelectedControlMethodDocumentationService : Service<BllSelectedControlMethodDocumentation, DalSelectedControlMethodDocumentation>, ISelectedControlMethodDocumentationService
    {
        private readonly IUnitOfWork uow;

        public SelectedControlMethodDocumentationService(IUnitOfWork uow) : base(uow, uow.SelectedControlMethodDocumentations)
        {
            this.uow = uow;
            bllMapper = new SelectedControlMethodDocumentationMapper(uow);
        }
        ISelectedControlMethodDocumentationMapper bllMapper;
        public override void Create(BllSelectedControlMethodDocumentation entity)
        {
            uow.SelectedControlMethodDocumentations.Create(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Update(BllSelectedControlMethodDocumentation entity)
        {
            uow.SelectedControlMethodDocumentations.Update(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllSelectedControlMethodDocumentation entity)
        {
            uow.SelectedControlMethodDocumentations.Delete(bllMapper.MapToDal(entity));
            uow.Commit();
        }

        public override IEnumerable<BllSelectedControlMethodDocumentation> GetAll()
        {
            var elements = uow.SelectedControlMethodDocumentations.GetAll();
            var retElemets = new List<BllSelectedControlMethodDocumentation>();
            foreach (var element in elements)
            {
                retElemets.Add(bllMapper.MapToBll(element));
            }
            return retElemets;
        }

        public override BllSelectedControlMethodDocumentation Get(int id)
        {
            return bllMapper.MapToBll(uow.SelectedControlMethodDocumentations.Get(id));
        }
    }
}
