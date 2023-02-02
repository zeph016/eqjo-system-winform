using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Configurations.Modules
{
    public class clsModLevelAssignment
    {
        public Int64 Id { get; set; }
        public Int64 ModuleId { get; set; }
        public String ModuleName { get; set; }
        public Int64 UserLevelId { get; set; }
        public String UserLevelName { get; set; }
        public bool AllowAdd { get; set; }
        public bool AllowEdit { get; set; }
        public bool AllowDelete { get; set; }
        public bool AllowView { get; set; }

        public List<clsModLevelAssignment> ListOfModLevelAssignment {get; set;}
        public clsModLevelAssignment()
        {
            Id = 0;
            ModuleId = 0;
            UserLevelId = 0;
            AllowAdd = true;
            AllowEdit = true;
            AllowDelete = true;
            AllowView = true;

            ListOfModLevelAssignment = new List<clsModLevelAssignment>();
        }
    }
}
