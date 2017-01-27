using BLL.Entities;
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
        }
    }
}
