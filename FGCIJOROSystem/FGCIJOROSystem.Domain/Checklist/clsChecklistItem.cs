using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Global
{
    public class clsChecklistItem
    {
        public Int64 Id{ get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }

        public clsChecklistItem()
       {
            Id = 0;
            Name = "";
            Description = "";
            Active = true;
        }
    }
}
