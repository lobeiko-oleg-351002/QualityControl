using AutoMapper;
using DAL.Entities;
using DAL.Mapping;
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
            var ormEntity = mapper.MapToOrm(entity);            
            ormEntity.protocolNumber = GetControlCountWithCurrentType(entity.ControlName_id.Value) + 1;
            return context.Set<Control>().Add(ormEntity);
        }

        

        public IEnumerable<DalControl> GetAllControlled()
        {
            var elements = context.Controls.Select(entity => entity.isControlled == true);
            var retElemets = new List<DalControl>();
            //foreach (var element in elements)
            //{
            //    retElemets.Add(mapper.MapToDal(element));
            //}
            return retElemets;
        }

        public IEnumerable<DalControl> GetAllUncontrolled()
        {
            var elements = context.Controls.Select(entity => entity.isControlled == false);
            var retElemets = new List<DalControl>();
            foreach (var element in elements)
            {
                //retElemets.Add(Mapper.Map<DalControl>(element));
            }
            return retElemets;
        }

        public DalControl GetControlByNumber(int number)
        {
            var ormEntity = context.Controls.FirstOrDefault(entity => entity.number == number);
            return mapper.MapToDal(ormEntity);
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
                retElemets.Add(mapper.MapToDal(element));
            }
            return retElemets;
        }

        ControlMapper mapper = new ControlMapper();

        public new void Delete(DalControl entity)
        {
            var ormEntity = context.Set<Control>().Single(Control => Control.id == entity.Id);
            context.Set<Control>().Remove(ormEntity);
        }

        public new DalControl Get(int id)
        {
            var ormEntity = context.Set<Control>().FirstOrDefault(Control => Control.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalControl> GetAll()
        {
            var elements = context.Set<Control>().Select(Control => Control);
            var retElemets = new List<DalControl>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalControl entity)
        {
            var ormEntity = context.Set<Control>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

    }
}
