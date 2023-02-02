using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.JobOrder
{
    public class clsJOTransactionLogs
    {
        public Int64 Id { get; set; }
        public String ChecklistNo { get; set; }
        public Int64 JOId { get; set; }
        public Int64 JONo { get; set; }
        public String JONoStr { get { return (Type == clsEnums.TypeOrder.JO ? "JO" : "RO") + RefYear.Substring(RefYear.Length - 2) + JONo.ToString("0000"); } }
        public String RefYear { get; set; }
        public DateTime JODate { get; set; }
        public Boolean EquipmentOnBranch { get; set; }
        public Boolean PartsRequest { get; set; }
        public clsEnums.TypeOrder Type { get; set; }
        public String Mileage { get; set; } = string.Empty;

        //Customer
        public clsEnums.CustomerType CustomerType { get; set; }
        public Int64 CustomerId { get; set; }
        public String CustomerName { get; set; }
        public String Location { get; set; }
        public String CustomerRemarks { get; set; }

        //Branch
        public Int64 BranchId { get; set; }
        public String BranchName { get; set; }

        //Equipment
        public clsEnums.EquipmentType ItemType { get; set; }
        public Int64 EquipmentId { get; set; }
        public String EquipmentCode { get; set; }
        public String EquipmentName { get; set; }
        public String EquipmentLocation { get; set; }
        public String EquipmentRemarks { get; set; }

        //Contractor
        public Int64 ContractorId { get; set; }
        public clsEnums.ContractorCategory ContractorCategory { get; set; }
        public clsEnums.ContractorType ContractorType { get; set; }
        public String ContractorName { get; set; }
        public String ContractorSectionHead { get; set; }

        //Drivers
        public Int64 DriverId { get; set; }
        public String DriverName { get; set; }

        //Status
        public clsEnums.JOROStatus Status { get; set; }
        //public String StatusName { get; set; }

        //List of JODetails
        public List<clsJODetailTransactionLogs> ListOfJODetailTransactionLogs { get; set; }
        //List of Mechanics
        public List<clsJOMechTransactionLogs> ListOfMechanicsTransactionLogs { get; set; }

        //LOGS
        public Int64 PrintCount { get; set; }
        public Int64 UserId { get; set; }
        public String UserName { get; set; }
        public String UserPosition { get; set; }
        public Int64 ApproveId { get; set; }
        public String ApproverName { get; set; }
        public String ApproverPosition { get; set; }
        public DateTime ? ApprovedDate { get; set; }
        public DateTime DateEncoded { get; set; }
        //public Int64 TotalDone { get; set; }
        
        public clsJOTransactionLogs()
        {
            Id = 0;
            JOId = 0;
            JONo = 0;
            Type = clsEnums.TypeOrder.JO;
            JODate = DateTime.Now;
            EquipmentOnBranch = true;
            PartsRequest = false;
            CustomerType = clsEnums.CustomerType.External;
            CustomerId = 0;
            BranchId = 0;
            ItemType = clsEnums.EquipmentType.Equipment;
            EquipmentId = 0;
            ContractorId = 0;
            DriverId = 0;
            DateEncoded = DateTime.Now;
            Status = clsEnums.JOROStatus.Generated;
            ApprovedDate = null;
            ListOfJODetailTransactionLogs = new List<clsJODetailTransactionLogs>();
            ListOfMechanicsTransactionLogs = new List<clsJOMechTransactionLogs>();
        }
        public clsJOTransactionLogs(clsJOTransactionLogs obj)
        {
            Id = obj.Id;
            JOId = obj.JOId;
            JONo = obj.JONo;
            Type = obj.Type;
            JODate = obj.JODate;
            EquipmentOnBranch = obj.EquipmentOnBranch;
            PartsRequest = obj.PartsRequest;
            CustomerType = obj.CustomerType;
            CustomerId = obj.CustomerId;
            BranchId = obj.BranchId;
            ItemType = obj.ItemType;
            EquipmentId = obj.EquipmentId;
            ContractorId = obj.ContractorId;
            DriverId = obj.DriverId;
            DateEncoded = obj.DateEncoded;
            ApprovedDate = obj.ApprovedDate;
            Status = obj.Status;
            ListOfJODetailTransactionLogs = obj.ListOfJODetailTransactionLogs;
            ListOfMechanicsTransactionLogs = obj.ListOfMechanicsTransactionLogs;
        }
    }
}
