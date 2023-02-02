using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Configurations.EquipmentType
{
    public class clsEquipmentType
    {
        public Int64 Id { get; set; }
        public Int64 EquipmentTypeId { get; set; }
        public Int64 EquipmentCode{ get; set; }
        public string Description { get; set; }
        public string PPETypeName { get; set; }

        public clsEquipmentType()
        {
            Id=0;
            EquipmentTypeId=0;
            EquipmentCode = 0;
            Description="";
            PPETypeName="";
        }
    }

}
