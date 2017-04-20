using BLL.Entities;
using BLL.Mapping.Interfaces;
using BLL.Services;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Mapping;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public class CertificateMapper : ICertificateMapper
    {
        IUnitOfWork uow;
        public CertificateMapper(IUnitOfWork uow)
        {
            this.uow = uow;
            controlNameService = new ControlNameService(uow);
            employeeService = new EmployeeService(uow);
        }

        public DalCertificate MapToDal(BllCertificate entity)
        {
            DalCertificate dalEntity = new DalCertificate
            {
                Id = entity.Id,
                CheckDate = entity.CheckDate,
                Duration = entity.Duration,
                Title = entity.Title,
                ControlName_id = entity.ControlName != null ? entity.ControlName.Id : (int?)null,
                Employee_id = entity.Employee != null ? entity.Employee.Id : (int?)null,
                
            };

            return dalEntity;
        }

        IControlNameService controlNameService;
        IEmployeeService employeeService;

        public BllCertificate MapToBll(DalCertificate entity)
        {
            BllCertificate bllEntity = new BllCertificate
            {
                Id = entity.Id,
                CheckDate = entity.CheckDate,
                Duration = entity.Duration,
                Title = entity.Title,
                ControlName = entity.ControlName_id != null ? controlNameService.Get((int)entity.ControlName_id) : null,
                Employee = entity.Employee_id != null ? employeeService.Get((int)entity.Employee_id) : null
            };

            return bllEntity;
        }
    }
}
