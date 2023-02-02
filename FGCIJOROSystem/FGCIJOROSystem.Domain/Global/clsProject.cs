using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Global
{
    public class clsProject
    {
        public Boolean CheckProject { get; set; }
        public Int64 Id { get; set; }
        public String ProjectId { get; set; }
        public String ProjectName { get; set; }
        public String Location { get; set; }
        public String ProjectShortcutName { get; set; }
        public clsEnums.CustomerCategory CustomerCategory { get; set; }
        public List<clsProject> ListOfProject { get; set; }
    }
}
