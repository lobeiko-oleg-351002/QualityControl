using DAL.Entities;
using DAL.Mapping.Interfaces;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping
{
    public class TemplateMapper : ITemplateMapper
    {
        public DalTemplate MapToDal(Template entity)
        {
            return new DalTemplate
            {
                Id = entity.id,
                ControlNameLib_id = entity.controlNameLib_id,
                Description = entity.description,
                EquipmentLib_id = entity.equipmentLib_id,
                ImageLib_id = entity.imageLib_id,
                Material_id = entity.material_id,
                Name = entity.name,
                RequirementDocumentationLib_id = entity.requirementDocumentationLib_id,
                WeldJoint_id = entity.weldJoint_id
            };
        }

        public Template MapToOrm(DalTemplate entity)
        {
            return new Template
            {
                id = entity.Id,
                controlNameLib_id = entity.ControlNameLib_id,
                description = entity.Description,
                equipmentLib_id = entity.EquipmentLib_id,
                imageLib_id = entity.ImageLib_id,
                material_id = entity.Material_id,
                name = entity.Name,
                requirementDocumentationLib_id = entity.RequirementDocumentationLib_id,
                weldJoint_id = entity.WeldJoint_id
            };
        }
    }
}
