using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Configurations.UnitsJORO
{
    public class clsUnitJORO
    {
        public Int64 Id { get; set; }
        public Int64 UnitId { get; set; }
        public String UnitName { get; set; }
        public double MinutesValue { get; set; }
        public Boolean IsActive { get; set; }
        public clsUnitJORO()
        {
            Id = 0;
            UnitId = 0;
            UnitName = "";
            MinutesValue = 0;
            IsActive = true;
        }
    }
}
