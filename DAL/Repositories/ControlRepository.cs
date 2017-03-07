using AutoMapper;
using DAL.Entities;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ControlRepository : Repository<DalControl, Control>, IControlRepository
    {
        private readonly ServiceDB context;
        public ControlRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public new Control Create(DalControl entity)
        {
            Mapper.CreateMap<DalControl, Control>();
            var ormEntity = Mapper.Map<Control>(entity);
            //ormEntity.ControlMethodsLib = context.ControlMethodsLibs.FirstOrDefault(e => e.id == ormEntity.controlMethodsLib_id);
            ormEntity.protocolNumber = GetControlCountWithCurrentType(entity.ControlName_id.Value) + 1;
            return context.Set<Control>().Add(ormEntity);
        }

        

        public IEnumerable<DalControl> GetAllControlled()
        {
            Mapper.CreateMap<Control, DalControl>();
            var elements = context.Controls.Select(entity => entity.isControlled == true);
            var retElemets = new List<DalControl>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<DalControl>(element));
            }
            return retElemets;
        }

        public IEnumerable<DalControl> GetAllUncontrolled()
        {
            Mapper.CreateMap<Control, DalControl>();
            var elements = context.Controls.Select(entity => entity.isControlled == false);
            var retElemets = new List<DalControl>();
            foreach (var element in elements)
            {
                retElemets.Add(Mapper.Map<DalControl>(element));
            }
            return retElemets;
        }

        public DalControl GetControlByNumber(int number)
        {
            Mapper.CreateMap<Control, DalControl>();
            var ormEntity = context.Controls.FirstOrDefault(entity => entity.number == number);
            return Mapper.Map<DalControl>(ormEntity);
        }

        public int GetControlCountWithCurrentType(int controlNameId)
        {
            var controls = context.Set<Control>().Where(entity => entity.controlName_id == controlNameId);
            return controls.Count();
        }

        public IEnumerable<DalControl> GetControlsByLibId(int id)
        {
            var elements = context.Set<Control>().Where(entity => entity.controlMethodsLib_id == id);
            var retElemets = new List<DalControl>();
            foreach (var element in elements)
            {
                Mapper.CreateMap<Control, DalControl>();
                retElemets.Add(Mapper.Map<DalControl>(element));
            }
            return retElemets;
        }

    }
}
