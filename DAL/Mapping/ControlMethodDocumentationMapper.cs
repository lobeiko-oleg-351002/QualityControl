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
    public class ControlMethodDocumentationMapper : IControlMethodDocumentationMapper
    {
        public DalControlMethodDocumentation MapToDal(ControlMethodDocumentation entity)
        {
            return new DalControlMethodDocumentation
            {
                Id = entity.id,
                ControlName_id = entity.controlName_id,
                Name = entity.name,
                Pressmark = entity.pressmark
            };
        }

        public ControlMethodDocumentation MapToOrm(DalControlMethodDocumentation entity)
        {
            return new ControlMethodDocumentation
            {
                id = entity.Id,
                controlName_id = entity.ControlName_id,
                name = entity.Name,
                pressmark = entity.Pressmark
            };
        }
    }
}