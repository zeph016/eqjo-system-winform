using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.EquipmentUR
{
    public class clsEquipmentType
    {
        public Boolean CheckEquipmentType { get; set; }
        public Int64 Id { get; set; }
        public String EquipmentTypeName { get; set; }
        public List<clsEquipmentType> ListOfEquipmentType { get; set; }
    }
}
