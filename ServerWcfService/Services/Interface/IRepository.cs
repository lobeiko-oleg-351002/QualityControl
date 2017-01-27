using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities.Interface;

namespace ServerWcfService.Services.Interface
{
    [ServiceContract]
    public interface IRepository<TUilEntity>
        where TUilEntity : IUilEntity
    {
        [OperationContract]
        IEnumerable<TUilEntity> GetAll();

        [OperationContract]
        TUilEntity Get(int id);

        [OperationContract]
        void Create(TUilEntity entity);

        [OperationContract]
        void Delete(TUilEntity entity);

        [OperationContract]
        void Update(TUilEntity entity);

    }
}
