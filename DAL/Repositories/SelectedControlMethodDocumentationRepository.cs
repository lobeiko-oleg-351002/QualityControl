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
    public class SelectedControlMethodDocumentationRepository : Repository<DalSelectedControlMethodDocumentation, SelectedControlMethodDocumentation>, ISelectedControlMethodDocumentationRepository
    {
        private readonly ServiceDB context;
        public SelectedControlMethodDocumentationRepository(ServiceDB context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<DalSelectedControlMethodDocumentation> GetControlMethodDocumentationsByLibId(int id)
        {
            Mapper.CreateMap<SelectedControlMethodDocumentation, DalSelectedControlMethodDocumentation>();
            var elements = context.Set<SelectedControlMethodDocumentation>().Where(entity => entity.controlMethodDocumentationLib_id == id);
            var retElemets = new List<DalSelectedControlMethodDocumentation>();
            foreach (var element in elements)
            {
                Mapper.CreateMap<SelectedControlMethodDocumentation, DalSelectedControlMethodDocumentation>();
                retElemets.Add(Mapper.Map<DalSelectedControlMethodDocumentation>(element));
            }
            return retElemets;
        }

        public new SelectedControlMethodDocumentation Create(DalSelectedControlMethodDocumentation entity)
        {
            Mapper.CreateMap<DalSelectedControlMethodDocumentation, SelectedControlMethodDocumentation>();
            var ormEntity = Mapper.Map<SelectedControlMethodDocumentation>(entity);
            ormEntity.ControlMethodDocumentationLib = context.ControlMethodDocumentationLibs.FirstOrDefault(e => e.id == ormEntity.controlMethodDocumentationLib_id);
            //ormEntity.SelectedControlMethodDocumentationLib.SelectedControlMethodDocumentation.Add(ormEntity);
            return context.Set<SelectedControlMethodDocumentation>().Add(Mapper.Map<SelectedControlMethodDocumentation>(entity));
        }
    }
}
