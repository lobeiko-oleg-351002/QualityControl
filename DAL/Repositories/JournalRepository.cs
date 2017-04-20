using AutoMapper;
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
    public class JournalRepository : Repository<DalJournal, Journal>, IJournalRepository
    {
        private readonly ServiceDB context;
        public JournalRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        JournalMapper mapper = new JournalMapper();

        public new void Delete(DalJournal entity)
        {
            var ormEntity = context.Set<Journal>().Single(Journal => Journal.id == entity.Id);
            context.Set<Journal>().Remove(ormEntity);
        }

        public new DalJournal Get(int id)
        {
            var ormEntity = context.Set<Journal>().FirstOrDefault(Journal => Journal.id == id);
            return ormEntity != null ? (mapper.MapToDal(ormEntity)) : null;
        }

        public new IEnumerable<DalJournal> GetAll()
        {
            var elements = context.Set<Journal>().Select(Journal => Journal);
            var retElemets = new List<DalJournal>();
            if (elements.Any())
            {
                foreach (var element in elements)
                {
                    retElemets.Add(mapper.MapToDal(element));
                }
            }

            return retElemets;
        }

        public new void Update(DalJournal entity)
        {
            var ormEntity = context.Set<Journal>().Find(entity.Id);
            if (ormEntity != null)
            {
                context.Entry(ormEntity).CurrentValues.SetValues(mapper.MapToOrm(entity));
            }
        }

        public new Journal Create(DalJournal entity)
        {
            var res = context.Set<Journal>().Add(mapper.MapToOrm(entity));
            return res;
        }
    }
}
