using AutoMapper;
using BLL.Entities;
using BLL.Services.Interface;
using ServerWcfService.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities;

namespace ServerWcfService.Services
{
    public class ControlMethodDocumentationRepository : Repository<UilControlMethodDocumentation, BllControlMethodDocumentation>, IControlMethodDocumentationRepository
    {
        private readonly IControlMethodDocumentationService controlMethodDocumentationService;

        public ControlMethodDocumentationRepository() : base(UiUnitOfWork.Instance.ControlMethodDocumentations)
        {
            controlMethodDocumentationService = UiUnitOfWork.Instance.ControlMethodDocumentations;
        }

        public UilControlMethodDocumentation GetControlMethodDocumentationByName(string name)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllControlMethodDocumentation, UilControlMethodDocumentation>();
            });
            return Mapper.Map<UilControlMethodDocumentation>(controlMethodDocumentationService.GetControlMethodDocumentationByName(name));
        }

        public override void Create(UilControlMethodDocumentation entity)
        {
            controlMethodDocumentationService.Create(MapUilToBll(entity));
        }

        public override void Delete(UilControlMethodDocumentation entity)
        {
            controlMethodDocumentationService.Delete(MapUilToBll(entity));
        }

        public override void Update(UilControlMethodDocumentation entity)
        {
            controlMethodDocumentationService.Update(MapUilToBll(entity));
        }

        public override IEnumerable<UilControlMethodDocumentation> GetAll()
        {
            var bllEntities = controlMethodDocumentationService.GetAll();
            var retElements = new List<UilControlMethodDocumentation>();
            foreach (var element in bllEntities)
            {
                retElements.Add(MapBllToUil(element));
            }
            return retElements;
        }

        public static UilControlMethodDocumentation MapBllToUil(BllControlMethodDocumentation bllEntity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllControlMethodDocumentation, UilControlMethodDocumentation>();
                cfg.CreateMap<BllControlName, UilControlName>();
            });
            UilControlMethodDocumentation uilEntity = Mapper.Map<UilControlMethodDocumentation>(bllEntity);
            return uilEntity;
        }

        public static BllControlMethodDocumentation MapUilToBll(UilControlMethodDocumentation entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UilControlMethodDocumentation, BllControlMethodDocumentation>();
                cfg.CreateMap<UilControlName, BllControlName>();
            });

            BllControlMethodDocumentation bllEntity = Mapper.Map<BllControlMethodDocumentation>(entity);
            return bllEntity;
        }
    }
}
