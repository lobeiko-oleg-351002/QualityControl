using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IService<TBllEntity>
        where TBllEntity : IBllEntity
    {
        IEnumerable<TBllEntity> GetAll();

        TBllEntity Get(int id);

        void Create(TBllEntity entity);

        void Delete(TBllEntity entity);

        void Update(TBllEntity entity);

    }
}
