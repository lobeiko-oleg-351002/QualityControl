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
    public class SelectedControlMethodDocumentationMapper : ISelectedControlMethodDocumentationMapper
    {
        public DalSelectedControlMethodDocumentation MapToDal(SelectedControlMethodDocumentation entity)
        {
            return new DalSelectedControlMethodDocumentation
            {
                Id = entity.id,
                ControlMethodDocumentationLib_id = entity.controlMethodDocumentationLib_id,
                ControlMethodDocumentation_id = entity.controlMethodDocumentation_id
            };
        }

        public SelectedControlMethodDocumentation MapToOrm(DalSelectedControlMethodDocumentation entity)
        {
            return new SelectedControlMethodDocumentation
            {
                id = entity.Id,
                controlMethodDocumentation_id = entity.ControlMethodDocumentation_id,
                controlMethodDocumentationLib_id = entity.ControlMethodDocumentationLib_id
            };
        }
    }
}
