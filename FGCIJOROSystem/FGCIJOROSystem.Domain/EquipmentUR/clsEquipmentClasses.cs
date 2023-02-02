using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.EquipmentUR
{
    public class clsEquipmentClasses
    {
        public Boolean CheckEquipment { get; set; }
        public Int64 Id { get; set; }
        public String EquipmentClass { get; set; }
        public List<clsEquipmentClasses>ListOfEquipmentClass { get; set; }
    }
}
