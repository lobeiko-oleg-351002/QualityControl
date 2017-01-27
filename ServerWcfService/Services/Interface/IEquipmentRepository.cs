using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities;

namespace ServerWcfService.Services.Interface
{
    [ServiceContract]
    public interface IEquipmentRepository : IRepository<UilEquipment>
    {
        [OperationContract]
        UilEquipment GetEquipmentByName(string name);

        [OperationContract]
        IEnumerable<UilEquipment> GetEquipmentByType(string type);

        [OperationContract]
        IEnumerable<UilEquipment> GetEquipmentByFactoryNumber(int number);

        [OperationContract]
        IEnumerable<UilEquipment> GetCheckedEquipment();

        [OperationContract]
        IEnumerable<UilEquipment> GetUncheckedEquipment();
    }
}
