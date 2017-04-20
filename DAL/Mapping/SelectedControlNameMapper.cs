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
    public class SelectedControlNameMapper : ISelectedControlNameMapper
    {
        public DalSelectedControlName MapToDal(SelectedControlName entity)
        {
            return new DalSelectedControlName
            {
                Id = entity.id,
                ControlNameLib_id = entity.controlNameLib_id,
                ControlName_id = entity.controlName_id
            };
        }

        public SelectedControlName MapToOrm(DalSelectedControlName entity)
        {
            return new SelectedControlName
            {
                id = entity.Id,
                controlName_id = entity.ControlName_id,
                controlNameLib_id = entity.ControlNameLib_id
            };
        }
    }
}
