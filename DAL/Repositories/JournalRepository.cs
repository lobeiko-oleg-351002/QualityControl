using AutoMapper;
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

        public new Journal Create(DalJournal entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DalJournal, Journal>();
            });
            var res = context.Set<Journal>().Add(Mapper.Map<Journal>(entity));
            return res;
        }

        public new Journal Update(DalJournal entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DalJournal, Journal>();
            });
            var res = context.Set<Journal>().Find(entity.Id);
            if (res != null)
            {
                context.Entry(res).CurrentValues.SetValues(Mapper.Map<Journal>(entity));
            }
            return res;
        }
    }
}
