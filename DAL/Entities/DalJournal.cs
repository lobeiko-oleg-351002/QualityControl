using DAL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DalJournal : IDalEntity
    {
        public int Id { get; set; }

        public DateTime? Request_date { get; set; }

        public DateTime? Control_date { get; set; }

        public int? Request_number { get; set; }

        public int? Industrial_object_id { get; set; }

        public int? Amount { get; set; }

        public string Size { get; set; }

        public int? Material_id { get; set; }

        public int? Weld_joint_id { get; set; }

        public string Welding_type { get; set; }

        public int? ControlMethodsLib_id { get; set; }

        public int? Component_id { get; set; }

        public int? Customer_id { get; set; }

        public string Contract { get; set; }

        public string Description { get; set; }

        public string User_Modifier_Login { get; set; }

        public int? User_Owner_id { get; set; }

        public DateTime? Modified_date { get; set; }
    }
}
