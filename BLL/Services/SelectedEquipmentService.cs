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
    public class SelectedEquipmentService : Service<BllSelectedEquipment, DalSelectedEquipment>, ISelectedEquipmentService
    {
        private readonly IUnitOfWork uow;

        public SelectedEquipmentService(IUnitOfWork uow) : base(uow, uow.SelectedEquipments)
        {
            this.uow = uow;
        }

    }
}
