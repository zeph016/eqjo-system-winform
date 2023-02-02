using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.Reports
{
    public class clsEquipmentHistoryReport
    {
        //Equipment
        public bool CheckEquipmentName { get; set; } 
        public clsEnums.EquipmentType ItemType { get; set; }
        public Int64 EquipmentId { get; set; }
        public String EquipmentCode { get; set; }
        public String EquipmentName { get; set; }
        public String EquipmentLocation { get; set; }
        public String EquipmentRemarks { get; set; }
        public String EquipmentClass { get; set; }
        //Report
        public DateTime WithdrawalDate { get; set; }
        public String ProductName{ get; set; }
        public String WithdrawalQuantity{ get; set; }
        public String UnitName { get; set; }
        public Int64 BranchId{ get; set; }
        public String BranchName{ get; set; }
        public String Project{ get; set; }
        public String EmployeeName{ get; set; }
        public String PONumber{ get; set; }
        public String PRSNo{ get; set; }
        public String WithdrawalSlipNumber{ get; set; }
        public String MDRSNo{ get; set; }
        public Int64 WasteStatusId{ get; set; }
        public String WasteStatus{ get; set; }
        public String Remarks { get; set; }
        public List<clsEquipmentHistoryReport> ListOfEquipment { get; set; } 
    }
}