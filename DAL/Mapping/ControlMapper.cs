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
    public class ControlMapper : IControlMapper
    {
        public DalControl MapToDal(Control entity)
        {
            return new DalControl
            {
                Id = entity.id,
                Additionally = entity.additionally,
                ControlMethodDocumentationLib_id = entity.controlMethodDocumentationLib_id,
                ControlMethodsLib_id = entity.controlMethodsLib_id,
                ControlName_id = entity.controlName_id,
                IsControlled = entity.isControlled,
                EmployeeLib_id = entity.employeeLib_id,
                EquipmentLib_id = entity.equipmentLib_id,
                ImageLib_id = entity.imageLib_id,
                Light = entity.light,
                Number = entity.number,
                ProtocolNumber = entity.protocolNumber,
                RequirementDocumentationLib_id = entity.requirementDocumentationLib_id,
                ResultLib_id = entity.resultLib_id
            };
        }

        public Control MapToOrm(DalControl entity)
        {
            return new Control
            {
                id = entity.Id,
                additionally = entity.Additionally,
                controlMethodDocumentationLib_id = entity.ControlMethodDocumentationLib_id,
                controlMethodsLib_id = entity.ControlMethodsLib_id,
                controlName_id = entity.ControlName_id,
                isControlled = entity.IsControlled,
                employeeLib_id = entity.EmployeeLib_id,
                equipmentLib_id = entity.EquipmentLib_id,
                imageLib_id = entity.ImageLib_id,
                light = entity.Light,
                number = entity.Number,
                protocolNumber = entity.ProtocolNumber,
                requirementDocumentationLib_id = entity.RequirementDocumentationLib_id,
                resultLib_id = entity.ResultLib_id
            };
        }
    }
}
