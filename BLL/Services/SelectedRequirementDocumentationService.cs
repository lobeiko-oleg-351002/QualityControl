using BLL.Entities;
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
        }
    }
}
