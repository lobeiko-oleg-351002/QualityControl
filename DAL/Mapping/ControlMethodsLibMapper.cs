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
    public class ControlMethodsLibMapper : IControlMethodsLibMapper
    {
        public DalControlMethodsLib MapToDal(ControlMethodsLib entity)
        {
            return new DalControlMethodsLib
            {
                Id = entity.id,
                
            };
        }

        public ControlMethodsLib MapToOrm(DalControlMethodsLib entity)
        {
            return new ControlMethodsLib
            {
                id = entity.Id
            };
        }
    }
}
