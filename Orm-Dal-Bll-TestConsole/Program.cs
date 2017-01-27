using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;
using DAL.Entities;
using ORM;

namespace Orm_Dal_Bll_TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            UnitOfWork uof = new UnitOfWork(new ServiceDB());
            var role = new DalRole
            {
                Name = "Ilya lox"
            };
            uof.Roles.Create(role);
            uof.Commit();
            IEnumerable<DalRole> roles = uof.Roles.GetAll();
            foreach(var role1 in roles)
            {
                Console.WriteLine(role1.Id);
            }
            Console.ReadKey();
        }
    }
}
