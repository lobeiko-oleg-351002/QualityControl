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
    public class ControlMethodDocumentationLibMapper : IControlMethodDocumentationLibMapper
    {
        public DalControlMethodDocumentationLib MapToDal(ControlMethodDocumentationLib entity)
        {
            return new DalControlMethodDocumentationLib
            {
                Id = entity.id,
            };
        }

        public ControlMethodDocumentationLib MapToOrm(DalControlMethodDocumentationLib entity)
        {
            return new ControlMethodDocumentationLib
            {
                id = entity.Id
            };
        }
    }
}