using AutoMapper;
using BLL.Entities;
using BLL.Services.Interface;
using ServerWcfService.Services.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities;
using UIL.Entities.Interface;

namespace ServerWcfService.Services
{
    public class JournalRepository : Repository<UilJournal, BllJournal>, IJournalRepository
    {
        private readonly IJournalService JournalService;

        public JournalRepository() : base(UiUnitOfWork.Instance.Journals)
        {
            JournalService = UiUnitOfWork.Instance.Journals;
        }

        public new UilJournal Create(UilJournal entity)
        {
            return MapBllToUil(JournalService.Create(MapUilToBll(entity)));
        }

        public new UilJournal Update(UilJournal entity)
        {
            return MapBllToUil(JournalService.Update(MapUilToBll(entity)));
        }

        public override void Delete(UilJournal entity)
        {
            JournalService.Delete(MapUilToBll(entity));
        }


        public override IEnumerable<UilJournal> GetAll()
        {
            var bllEntities = JournalService.GetAll();
            var retElements = new List<UilJournal>();
            foreach (var element in bllEntities)
            {
                //var watch = System.Diagnostics.Stopwatch.StartNew();
                retElements.Add(MapBllToUil(element));
                //watch.Stop();
               // var elapsedMs = watch.ElapsedMilliseconds;

            }
            return retElements;
        }

        public override UilJournal Get(int id)
        {
            var bllEntity = JournalService.Get(id);
            return MapBllToUil(bllEntity);
        }

        public UilJournal MapBllToUil(BllJournal bllEntity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllMaterial, UilMaterial>();
                cfg.CreateMap<BllJournal, UilJournal>().ForMember(x => x.ControlMethodsLib, opt => opt.Ignore())
                                                       .ForMember(x => x.Component, opt => opt.Ignore())
                                                       .ForMember(x => x.IndustrialObject, opt => opt.Ignore());
                cfg.CreateMap<BllCustomer, UilCustomer>();
                cfg.CreateMap<BllWeldJoint, UilWeldJoint>();
                cfg.CreateMap<BllControlMethodsLib, UilControlMethodsLib>();
            });
            UilJournal uilEntity = Mapper.Map<UilJournal>(bllEntity);

            //List<Task> tasks = new List<Task>();

            //if (bllEntity.Component != null)
            //{
            //    Task<UilComponent> mapComponentTask = Task.Run(() => ComponentRepository.MapBllToUil(bllEntity.Component));
            //    tasks.Add(mapComponentTask.ContinueWith(
            //        (TResult) => uilEntity.Component = TResult.Result));
            //}

            uilEntity.Component = bllEntity.Component != null ? ComponentRepository.MapBllToUil(bllEntity.Component) : null;

            //Task<UilControlMethodsLib> mapControlMethodsLibTask = Task.Run(() => {
            //    UilControlMethodsLib target = null;
            //    Mapper.Initialize(cfg =>
            //    {
            //        cfg.CreateMap<BllControlMethodsLib, UilControlMethodsLib>().ForMember(x => x.Control, opt => opt.Ignore());
            //    });
            //    target = Mapper.Map<UilControlMethodsLib>(bllEntity.ControlMethodsLib);

            //    if (target != null)
            //    {
            //        foreach (var bllControl in bllEntity.ControlMethodsLib.Control)
            //        {
            //            target.Control.Add(ControlRepository.MapBllToUil(bllControl));
            //        }
            //    }

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllControlMethodsLib, UilControlMethodsLib>().ForMember(x => x.Control, opt => opt.Ignore());
            });
           uilEntity.ControlMethodsLib = Mapper.Map<UilControlMethodsLib>(bllEntity.ControlMethodsLib);

            if (uilEntity.ControlMethodsLib != null)
            {
                foreach (var bllControl in bllEntity.ControlMethodsLib.Control)
                {
                    uilEntity.ControlMethodsLib.Control.Add(ControlRepository.MapBllToUil(bllControl));
                }
            }

            //    return target;
            //});
            //tasks.Add(mapControlMethodsLibTask.ContinueWith((TResult) => uilEntity.ControlMethodsLib = TResult.Result));

            //if (bllEntity.IndustrialObject != null)
            //{
            //    Task<UilIndustrialObject> mapIndustrialObjectTask = Task.Run(() => IndustrialObjectRepository.MapBllToUil(bllEntity.IndustrialObject));
            //    tasks.Add(mapIndustrialObjectTask.ContinueWith(
            //        (TResult) => uilEntity.IndustrialObject = TResult.Result));
            //}
            uilEntity.IndustrialObject = bllEntity.IndustrialObject != null ? IndustrialObjectRepository.MapBllToUil(bllEntity.IndustrialObject) : null;

            //if (bllEntity.Material != null)
            //{
            //    Task<UilMaterial> mapMaterialTask = Task.Run(() => {
            //        Mapper.Initialize(cfg =>
            //        {
            //            cfg.CreateMap<BllMaterial, UilMaterial>();
            //        });
            //        return Mapper.Map<UilMaterial>(bllEntity.Material);
            //    });
            //    tasks.Add(mapMaterialTask.ContinueWith((TResult) => uilEntity.Material = TResult.Result));
            //}


            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllMaterial, UilMaterial>();
            });
            uilEntity.Material = Mapper.Map<UilMaterial>(bllEntity.Material);

            //if (bllEntity.WeldJoint != null)
            //{
            //    Task<UilWeldJoint> mapWeldJointTask = Task.Run(() => {
            //        Mapper.Initialize(cfg =>
            //        {
            //            cfg.CreateMap<BllWeldJoint, UilWeldJoint>();
            //        });
            //        return Mapper.Map<UilWeldJoint>(bllEntity.WeldJoint);
            //    });
            //    tasks.Add(mapWeldJointTask.ContinueWith((TResult) => uilEntity.WeldJoint = TResult.Result));
            //}

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BllWeldJoint, UilWeldJoint>();
            });
            uilEntity.WeldJoint = Mapper.Map<UilWeldJoint>(bllEntity.WeldJoint);

            //await Task.WhenAll(tasks);



            return uilEntity;
        }

        public static BllJournal MapUilToBll(UilJournal entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UilMaterial, BllMaterial>();
                cfg.CreateMap<UilJournal, BllJournal>().ForMember(x => x.ControlMethodsLib, opt => opt.Ignore())
                                                       .ForMember(x => x.Component, opt => opt.Ignore())
                                                       .ForMember(x => x.IndustrialObject, opt => opt.Ignore());
                cfg.CreateMap<UilCustomer, BllCustomer>();
                cfg.CreateMap<UilWeldJoint, BllWeldJoint>();
                cfg.CreateMap<UilControlMethodsLib, BllControlMethodsLib>().ForMember(x => x.Control, opt => opt.Ignore());
            });

            BllJournal bllEntity = Mapper.Map<BllJournal>(entity);
            bllEntity.Component = entity.Component != null ? ComponentRepository.MapUilToBll(entity.Component) : null;
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UilControlMethodsLib, BllControlMethodsLib>().ForMember(x => x.Control, opt => opt.Ignore());
            });
            bllEntity.ControlMethodsLib = Mapper.Map<BllControlMethodsLib>(entity.ControlMethodsLib);
            if (entity.ControlMethodsLib != null)
            {
                foreach (var uilControl in entity.ControlMethodsLib.Control)
                {
                    bllEntity.ControlMethodsLib.Control.Add(ControlRepository.MapUilToBll(uilControl));
                }
            }
            bllEntity.IndustrialObject = entity.IndustrialObject != null ? IndustrialObjectRepository.MapUilToBll(entity.IndustrialObject) : null;
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UilMaterial, BllMaterial>();
            });
            bllEntity.Material = Mapper.Map<BllMaterial>(entity.Material);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UilWeldJoint, BllWeldJoint>();
            });
            bllEntity.WeldJoint = Mapper.Map<BllWeldJoint>(entity.WeldJoint);
            return bllEntity;
        }
    }
}
