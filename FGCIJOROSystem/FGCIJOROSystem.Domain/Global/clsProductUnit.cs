using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Global
{
    public class clsProductUnit
    {
        public Int64 Id { get; set; }
        public String UnitName { get; set; }
        public String Remarks { get; set; }
        public String UnitShortName { get; set; }
        public Boolean IsActive { get; set; }
    }
}
