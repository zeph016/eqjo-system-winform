using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.PPEStatusLog
{
    public class clsPPEStatusLog
    {
        public Int64 EquipmentId { get; set; }
        public String EquipmentCode { get; set; }
        public String EquipmentName { get; set; }
        public String Location { get; set; }
        public Int64 EquipmentStatusId { get; set; }
        public String EquipmentStatusName { get; set; }

        public Int64 PPETypeId { get; set; }
        public String PPETypeName { get; set; }
        public Int64 PPEClassId { get; set; }
        public String PPEClassName { get; set; }

        public Int64 EmployeeId { get; set; }
        public String FullName { get; set; }
        public String SystemName { get; set; }
        public DateTime DateUpdate { get; set; }
        public DateTime TimeUpdate { get; set; }
        public List<clsPPEStatusLog> ListOfPPEStatusLog { get; set; }

        public string PlateNumber { get; set; }

        public clsPPEStatusLog()
        {
            EquipmentId = 0;
            PPETypeId = 0;
            PPEClassId = 0;
            EquipmentId = 0;
            EquipmentStatusId = 0;
            EmployeeId = 0;
            SystemName = "";
            DateUpdate = DateTime.Now;
            TimeUpdate = DateTime.Now;
            ListOfPPEStatusLog = new List<clsPPEStatusLog>();
        }
    }
}
