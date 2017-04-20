using DAL.Entities;
using DAL.Mapping.Interfaces;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping
{
    public class CertificateMapper : ICertificateMapper
    {
        public DalCertificate MapToDal(Certificate entity)
        {
            return new DalCertificate
            {
                Id = entity.id,
                CheckDate = entity.checkDate,
                ControlName_id = entity.controlName_id,
                Duration = entity.duration,
                Employee_id = entity.employee_id,
                Title = entity.title
            };
        }

        public Certificate MapToOrm(DalCertificate entity)
        {
            return new Certificate
            {
                id = entity.Id,
                checkDate = entity.CheckDate,
                controlName_id = entity.ControlName_id,
                duration = entity.Duration,
                employee_id = entity.Employee_id,
                title = entity.Title
            };
        }
    }
}
