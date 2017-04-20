using AutoMapper;
using BLL.Entities;
using BLL.Mapping;
using BLL.Mapping.Interfaces;
using BLL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ComponentLibService : Service<BllComponentLib, DalComponentLib>, IComponentLibService
    {
        private readonly IUnitOfWork uow;


        ComponentLibMapper bllMapper;
        DAL.Mapping.ComponentLibMapper dalMapper;

        public ComponentLibService(IUnitOfWork uow) : base(uow, uow.ComponentLibs)
        {
            this.uow = uow;
            bllMapper = new ComponentLibMapper(uow);
            dalMapper = new DAL.Mapping.ComponentLibMapper();
        }

        public new BllComponentLib Create(BllComponentLib entity)
        {
            var ormEntity = uow.ComponentLibs.Create(bllMapper.MapToDal(entity));
            uow.Commit();
            entity.Id = ormEntity.id;
            ISelectedComponentMapper selectedComponentMapper = new SelectedComponentMapper(uow);
            foreach (var Component in entity.SelectedComponent)
            {
                var dalComponent = selectedComponentMapper.MapToDal(Component);
                dalComponent.ComponentLib_id = entity.Id;
                var ormComponent = uow.SelectedComponents.Create(dalComponent);
                uow.Commit();
                Component.Id = ormComponent.id;
            }

            return entity;
        }

        public override BllComponentLib Get(int id)
        {
            return bllMapper.MapToBll(uow.ComponentLibs.Get(id));
        }

        public override void Update(BllComponentLib entity)
        {
            ISelectedComponentMapper selectedComponentMapper = new SelectedComponentMapper(uow);
            foreach (var Component in entity.SelectedComponent)
            {
                if (Component.Id == 0)
                {
                    var dalComponent = selectedComponentMapper.MapToDal(Component);
                    dalComponent.ComponentLib_id = entity.Id;
                    uow.SelectedComponents.Create(dalComponent);
                }
            }
            var ComponentsWithLibId = uow.SelectedComponents.GetComponentsByLibId(entity.Id);
            foreach (var Component in ComponentsWithLibId)
            {
                bool isTrashComponent = true;
                foreach (var selectedComponent in entity.SelectedComponent)
                {
                    if (Component.Id == selectedComponent.Id)
                    {
                        isTrashComponent = false;
                        break;
                    }
                }
                if (isTrashComponent == true)
                {
                    uow.SelectedComponents.Delete(Component);
                }
            }
            uow.Commit();
        }

    }
}
