using BLL.Entities;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interface
{
    public interface IControlMethodsLibService : IService<BllControlMethodsLib>
    {
        new BllControlMethodsLib Create(BllControlMethodsLib entity);
        new BllControlMethodsLib Update(BllControlMethodsLib entity);
    }
}
