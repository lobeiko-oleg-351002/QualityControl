using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class BllControlMethodDocumentationLib : IBllEntity
    {
        public int Id { get; set; }

        public List<BllSelectedControlMethodDocumentation> SelectedControlMethodDocumentation { get; set; }

        public BllControlMethodDocumentationLib()
        {
            SelectedControlMethodDocumentation = new List<BllSelectedControlMethodDocumentation>();
        }
    }
}
