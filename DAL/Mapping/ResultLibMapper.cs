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
    public class ResultLibMapper : IResultLibMapper
    {
        public DalResultLib MapToDal(ResultLib entity)
        {
            return new DalResultLib
            {
                Id = entity.id,
            };
        }

        public ResultLib MapToOrm(DalResultLib entity)
        {
            return new ResultLib
            {
                id = entity.Id
            };
        }
    }
}
