using AutoMapper;
using BLL.Entities;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmployeeService : Service<BllEmployee, DalEmployee>, IEmployeeService
    {
        private readonly IUnitOfWork uow;

        public EmployeeService(IUnitOfWork uow) : base(uow, uow.Employees)
        {
            this.uow = uow;
        }

        public override void Create(BllEmployee entity)
        {
            //CertificateLibService CertificateLibService = new CertificateLibService(uow);
            //var CertificateLib = CertificateLibService.Create(entity.CertificateLib);
            //entity.CertificateLib = CertificateLib;
            uow.Employees.Create(MapBllToDal(entity));
            uow.Commit();
        }

        public override void Delete(BllEmployee entity)
        {
            uow.Employees.Delete(MapBllToDal(entity));
            uow.Commit();
        }

        public override void Update(BllEmployee entity)
        {
            //CertificateLibService CertificateLibService = new CertificateLibService(uow);
            //CertificateLibService.Update(entity.CertificateLib);
            uow.Employees.Update(MapBllToDal(entity));
            uow.Commit();
        }

        public override BllEmployee Get(int id)
        {
            DalEmployee dalEntity = uow.Employees.Get(id);
            return MapDalToBll(dalEntity);
        }

        public override IEnumerable<BllEmployee> GetAll()
        {
            var elements = uow.Employees.GetAll();
            var retElemets = new List<BllEmployee>();
            foreach (var element in elements)
            {
                retElemets.Add(MapDalToBll(element));
            }
            return retElemets;
        }

        public IEnumerable<BllEmployee> GetEmployeesByFatherName(string name)
        {
            Mapper.CreateMap<DalEmployee, BllEmployee>();
            var elements = uow.Employees.GetEmployeesByFatherName(name);
            var retElemets = new List<BllEmployee>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<BllEmployee>(element));
            }
            return retElemets;
        }

        public IEnumerable<BllEmployee> GetEmployeesByFunction(string function)
        {
            Mapper.CreateMap<DalEmployee, BllEmployee>();
            var elements = uow.Employees.GetEmployeesByFunction(function);
            var retElemets = new List<BllEmployee>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<BllEmployee>(element));
            }
            return retElemets;
        }

        public IEnumerable<BllEmployee> GetEmployeesByName(string name)
        {
            Mapper.CreateMap<DalEmployee, BllEmployee>();
            var elements = uow.Employees.GetEmployeesByName(name);
            var retElemets = new List<BllEmployee>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<BllEmployee>(element));
            }
            return retElemets;
        }

        public IEnumerable<BllEmployee> GetEmployeesBySirname(string name)
        {
            Mapper.CreateMap<DalEmployee, BllEmployee>();
            var elements = uow.Employees.GetEmployeesBySirname(name);
            var retElemets = new List<BllEmployee>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<BllEmployee>(element));
            }
            return retElemets;
        }

        private DalEmployee MapBllToDal(BllEmployee entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllEmployee, DalEmployee>();
            });

            DalEmployee dalEntity = Mapper.Map<DalEmployee>(entity);
            //dalEntity.Certificate_lib_id = entity.CertificateLib != null ? entity.CertificateLib.Id : (int?)null;
            return dalEntity;
        }

        private BllEmployee MapDalToBll(DalEmployee entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DalEmployee, BllEmployee>();
            });
            BllEmployee bllEmployee = Mapper.Map<BllEmployee>(entity);
            //CertificateLibService certificateLibService = new CertificateLibService(uow);
            //bllEmployee.CertificateLib = entity.Certificate_lib_id != null ? certificateLibService.Get((int)entity.Certificate_lib_id) : null;
            return bllEmployee;
        }
    }
}
