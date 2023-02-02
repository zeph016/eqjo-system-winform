using FGCIJOROSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGCIJOROSystem.Domain.RescueOrder
{
    public class clsROTransactionLogs
    {
         public Int64 Id { get; set; }
         public Int64 ROId { get; set; }
        public Int64 RONo { get; set; }
        public String RONoStr { get { return (Type == clsEnums.TypeOrder.RO ? "JO" : "RO") + RefYear.Substring(RefYear.Length - 2) + RONo.ToString("0000"); } }
        public String RefYear { get; set; }
        public DateTime RODate { get; set; }
        public clsEnums.TypeOrder Type { get; set; }
        public Boolean IsJobOut { get; set; }
        public String ChecklistNo { get; set; }
        public String Mileage { get; set; } = string.Empty;
        //Customer
        public clsEnums.CustomerCategory CustomerCategory { get; set; }
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

        //Service Drivers
        public Int64 DriverId { get; set; }
        public String DriverName { get; set; }

        //Service Drivers
        public Int64 ServiceDriverId { get; set; }
        public String ServiceDriverName { get; set; }

        //Service Vehicle
        public Int64 ServiceVehicleId { get; set; }
        public String ServiceVehicleName { get; set; }
        //Status
        public clsEnums.JOROStatus Status { get; set; }

        //List of JODetails
        public List<clsRODetailTransactionLogs> ListOfRODetailTransactionLogs { get; set; }
        //List of Mechanics
        public List<clsROMechTransactionLogs> ListOfROMechanicsTransactionLogs { get; set; }

        //LOGS
        public Int64 PrintCount { get; set; }
        public Int64 UserId { get; set; }
        public String UserName { get; set; }
        public String UserPosition { get; set; }
        public Int64 ApproverId { get; set; }
        public String ApproverName { get; set; }
        public String ApproverPosition { get; set; }
        public DateTime ? ApprovedDate { get; set; }
        public DateTime DateEncoded { get; set; }
        //public Int64 TotalDone { get; set; }
        
        public clsROTransactionLogs()
        {
            Id = 0;
            RONo = 0;
            ChecklistNo = "";
            Type = clsEnums.TypeOrder.RO;
            RODate = DateTime.Now;
            IsJobOut = false;
            CustomerType = clsEnums.CustomerType.External;
            CustomerId = 0;
            BranchId = 0;
            ItemType = clsEnums.EquipmentType.Equipment;
            EquipmentId = 0;
            ContractorId = 0;
            DriverId = 0;
            ServiceDriverId = 0;
            ServiceVehicleId = 0;
            DateEncoded = DateTime.Now;
            Status = clsEnums.JOROStatus.Generated;
            ApprovedDate = null;
            ListOfRODetailTransactionLogs = new List<clsRODetailTransactionLogs>();
            ListOfROMechanicsTransactionLogs = new List<clsROMechTransactionLogs>();
        }
        public clsROTransactionLogs(clsROTransactionLogs obj)
        {
            Id = obj.Id;
            ROId = obj.ROId;
            RONo = obj.RONo;
            Type = obj.Type;
            ChecklistNo = obj.ChecklistNo;
            RODate = obj.RODate;
            CustomerType = obj.CustomerType;
            CustomerId = obj.CustomerId;
            BranchId = obj.BranchId;
            ItemType = obj.ItemType;
            EquipmentId = obj.EquipmentId;
            ContractorId = obj.ContractorId;
            DriverId = obj.DriverId;
            ServiceDriverId = obj.ServiceDriverId;
            ServiceVehicleId = obj.ServiceVehicleId;
            DateEncoded = obj.DateEncoded;
            ApprovedDate = obj.ApprovedDate;
            Status = obj.Status;
            ListOfRODetailTransactionLogs = obj.ListOfRODetailTransactionLogs;
            ListOfROMechanicsTransactionLogs = obj.ListOfROMechanicsTransactionLogs;
        }
    }
}
