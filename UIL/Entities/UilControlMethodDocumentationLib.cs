using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIL.Entities.Interface;

namespace UIL.Entities
{
    public class UilControlMethodDocumentationLib : IUilEntity
    {
        public int Id { get; set; }

        public List<UilSelectedControlMethodDocumentation> SelectedControlMethodDocumentation { get; set; }

        public UilControlMethodDocumentationLib()
        {
            SelectedControlMethodDocumentation = new List<UilSelectedControlMethodDocumentation>();
        }
    }
}
