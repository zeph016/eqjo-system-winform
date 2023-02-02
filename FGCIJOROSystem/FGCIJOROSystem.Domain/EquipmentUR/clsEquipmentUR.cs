using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.EquipmentUR
{
    public class clsEquipmentUR
    {
        public Int64  Id { get; set; }
        public Int64 EquipmentMasterlistId { get; set; }
        public clsEnums.EquipmentType EquipmentType { get; set; }        
        public String EquipmentClass { get; set; }
        public String EquipmentCode { get; set; }
        public String EquipmentName { get; set; }
        public String Location { get; set; }
        public String Driver { get; set; }
        public String PlateNo { get; set; }
        public Int64 EquipmentStatusId{ get; set; }
        public String EquipmentStatus{ get; set; }
        public String JORONos { get; set; }
        public List<clsEquipmentUR> ListOfEquipmentUR { get; set; }
        public clsEquipmentUR()
        {
            EquipmentMasterlistId = 0;
            EquipmentType = clsEnums.EquipmentType.Equipment;
            EquipmentClass = "";
            EquipmentCode = "";
            EquipmentName = "";
            Location = "";
            Driver = "";
            PlateNo = "";
            EquipmentStatusId = 0;
            EquipmentStatus = "";
            JORONos = "";
            ListOfEquipmentUR = new List<clsEquipmentUR>();
        }
    }
}
